using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSync.Log
{
    public class LogFile
    {
        public List<LogEntry> entries { get; set; }=new List<LogEntry>();

        public byte[] ToLogBytes()
        {
            byte[] logBytes = new byte[0];
            int length = 0;
            int alloffset = 0;
            foreach (var entry in entries)
            {
                length += entry.ToEntryBytes().Length;
            }
            alloffset = 5 * entries.Count;
            logBytes = new byte[length + 100 + alloffset];
            byte[] header = new byte[90]
            {
                0x52, 0x55, 0x42, 0x59, 0x4C, 0x4F, 0x47, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x54, 0x48, 0x49, 0x53, 0x20, 0x49, 0x53, 0x20, 0x52, 0x41, 0x57, 0x20, 0x44, 0x41, 0x54, 0x41, 0x20,
                0x4F, 0x46, 0x20, 0x4C, 0x4F, 0x47, 0x20, 0x46, 0x49, 0x4C, 0x45, 0x2C, 0x20, 0x44, 0x4F, 0x20, 0x4E,
                0x4F, 0x54, 0x20, 0x4F, 0x50, 0x45, 0x4E, 0x20, 0x54, 0x48, 0x49, 0x53, 0x20, 0x46, 0x49, 0x4C, 0x45,
                0x20, 0x57, 0x49, 0x54, 0x48, 0x20, 0x54, 0x45, 0x58, 0x54, 0x20, 0x45, 0x44, 0x49, 0x54, 0x4F, 0x52,
                0x53, 0x2E, 0x00, 0x00, 0x00
            };
            header.CopyTo(logBytes, 0);

            //日志数据库格式版本号1.0
            new byte[] {1, 0}.CopyTo(logBytes, 90);

            //处理日志计数器
            string iStr = entries.Count.ToString("X8");

            for (int i = 0; i < iStr.Length; i += 2)
            {
                logBytes[92 + i / 2] = byte.Parse(iStr[i].ToString() + iStr[i + 1].ToString(),
                    NumberStyles.AllowHexSpecifier);
            }

            //处理日志全局偏移量
            string aStr = alloffset.ToString("X8");

            for (int i = 0; i < iStr.Length; i += 2)
            {
                logBytes[96 + i / 2] = byte.Parse(aStr[i].ToString() + aStr[i + 1].ToString(),
                    NumberStyles.AllowHexSpecifier);
            }
            int dataoffset = 100;
            int offset = 100 + alloffset;

            //开始处理日志数据
            for (int index = 0; index < entries.Count; index++)
            {
                LogEntry entry = entries[index];
                byte[] eBytes = entry.ToEntryBytes();
                eBytes.CopyTo(logBytes, offset);

                //记录日志偏移量
                string oStr = offset.ToString("X8");
                byte[] data = new byte[5];

                for (int o = 0; o < oStr.Length; o += 2)
                {
                    data[o / 2] = byte.Parse(oStr[o].ToString() + oStr[o + 1].ToString(),
                        NumberStyles.AllowHexSpecifier);
                }
                data[4] = (byte) ',';
                data.CopyTo(logBytes, dataoffset);

                //记录指针后移
                offset += eBytes.Length;
                dataoffset += 5;
            }


            return logBytes;
        }

        public static LogFile FromLogBytes(byte[] logBytes)
        {
            LogFile log = new LogFile();
            int elng = 0, odlng = 0;
            byte[] eblng = new byte[4], odblng = new byte[4];

            //Parse eblng
            for (int i = 0; i < eblng.Length; i++)
            {
                eblng[i] = logBytes[92 + i];
            }
            string estr = "";
            foreach (var e in eblng)
            {
                estr += e.ToString("X2");
            }
            elng = int.Parse(estr, NumberStyles.AllowHexSpecifier);

            //Parse odblng
            for (int i = 0; i < odblng.Length; i++)
            {
                odblng[i] = logBytes[96 + i];
            }
            string odstr = "";
            foreach (var od in odblng)
            {
                odstr += od.ToString("X2");
            }
            odlng = int.Parse(odstr, NumberStyles.AllowHexSpecifier) / 5;

            //Parse Offsets
            int odoffset = 100;
            List<int> dataOffsets = new List<int>();
            for (int i = 0; i < odlng; i++)
            {
                int od = odoffset + 5 * i;
                dataOffsets.Add(int.Parse(
                    $@"{logBytes[od]:X2}{logBytes[od + 1]:X2}{logBytes[od + 2]:X2}{logBytes[od + 3]:X2}",
                    NumberStyles.AllowHexSpecifier));
            }

            //Parse Datas
            List<byte[]> logdatas = new List<byte[]>();
            List<byte> logdata = new List<byte>();
            for (int i = 100 + 5 * dataOffsets.Count; i <= logBytes.Length; i++)
            {

                if (i == logBytes.Length)
                {
                    if (logdata.Count > 0)
                    {
                        logdatas.Add(logdata.ToArray());
                        break;
                    }
                }
                else if (dataOffsets.Contains(i))
                {

                    if (logdata.Count > 0)
                    {
                        logdatas.Add(logdata.ToArray());
                    }
                    logdata = new List<byte>();
                }
                logdata.Add(logBytes[i]);
            }
            List<LogEntry> entries = new List<LogEntry>();
            foreach (var logd in logdatas)
            {
                entries.Add(LogEntry.FromEntryBytes(logd));
            }
            log.entries = entries;
            return log;
        }
    }
}
