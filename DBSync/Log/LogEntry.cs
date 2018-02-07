using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSync.Log
{
    public class LogEntry
    {
        public LogLevel LogLevel { get; set; }=LogLevel.Warn;

        public DateTime LogTime { get; set; }=DateTime.Now;

        public string Source { get; set; } = "";

        public string Detail { get; set; } = "";

        public byte[] ToEntryBytes()
        {
            byte[] enBytes, srcBytes = Encoding.UTF8.GetBytes(Source), detailBytes = Encoding.UTF8.GetBytes(Detail);
            //日志长度定义:
            //日志长度(4字节) + 日志来源长度（4字节）+ 日志内容长度（4字节）+ 日志类型（1字节）+日志时间（8字节）+ 
            //日志来源（X 字节）+日志内容（Y字节）+日志结束标记（2字节）
            int length = 12 + 1 + 8 + srcBytes.Length + detailBytes.Length;
            enBytes = new byte[length + 2];

            //PARSE LENGTH
            string lenStr = length.ToString("X8");

            for (int i = 0; i < lenStr.Length; i += 2)
            {
                enBytes[i / 2] = byte.Parse(lenStr[i].ToString() + lenStr[i + 1].ToString(),
                    NumberStyles.AllowHexSpecifier);
            }

            //PARSE SRC LENGTH
            lenStr = srcBytes.Length.ToString("X8");

            for (int i = 0; i < lenStr.Length; i += 2)
            {
                enBytes[4 + i / 2] = byte.Parse(lenStr[i].ToString() + lenStr[i + 1].ToString(),
                    NumberStyles.AllowHexSpecifier);
            }

            //PARSE DETAIL LENGTH
            lenStr = detailBytes.Length.ToString("X8");

            for (int i = 0; i < lenStr.Length; i += 2)
            {
                enBytes[8 + i / 2] = byte.Parse(lenStr[i].ToString() + lenStr[i + 1].ToString(),
                    NumberStyles.AllowHexSpecifier);
            }

            //PARSE TYPE
            enBytes[12] = (byte)LogLevel;

            //Parse DateTime
            string dtStr = LogTime.ToBinary().ToString("X16");
            for (int i = 0; i < dtStr.Length; i += 2)
            {
                enBytes[13 + i / 2] = byte.Parse(dtStr[i].ToString() + dtStr[i + 1].ToString(),
                    NumberStyles.AllowHexSpecifier);
            }

            //Parse Source
            srcBytes.CopyTo(enBytes, 21);

            //Parse Data
            detailBytes.CopyTo(enBytes, 21 + srcBytes.Length);

            enBytes[enBytes.Length - 2] = 0x55;
            enBytes[enBytes.Length - 1] = 0xAA;
            return enBytes;
        }

        public static LogEntry FromEntryBytes(byte[] entryBytes)
        {
            #region init bytes
            LogEntry log=new LogEntry();
            byte[] length=new byte[4];
            byte[] lngsrc=new byte[4];
            byte[] lngdetail=new byte[4];
            byte[] type=new byte[1];
            byte[] dateBytes=new byte[8];

            //Parse lngsrc
            for (int i = 0; i < lngsrc.Length; i++)
            {
                lngsrc[i] = entryBytes[4 + i];
            }

            string srclngStr = "";
            foreach (var lng in lngsrc)
            {
                srclngStr += lng.ToString("X2");
            }
            int lngSRC = int.Parse(srclngStr,NumberStyles.AllowHexSpecifier);

            byte[] srcBytes=new byte[lngSRC];

            //Parse lngdetail
            for (int i = 0; i < lngdetail.Length; i++)
            {
                lngdetail[i] = entryBytes[8 + i];
            }

            string detlngStr = "";
            foreach (var lng in lngdetail)
            {
                detlngStr += lng.ToString("X2");
            }
            int detsrc = int.Parse(detlngStr,NumberStyles.AllowHexSpecifier);

            byte[] detailBytes=new byte[detsrc];

            #endregion

            type[0] = entryBytes[12];
            log.LogLevel = (LogLevel)type[0];

            //Parse Time
            for (int i = 0; i < dateBytes.Length; i++)
            {
                dateBytes[i] = entryBytes[13 + i];
            }
            string dt = "";
            for (int i = 0; i<dateBytes.Length; i++)
            {
                dt += dateBytes[i].ToString("X2");
            }
            log.LogTime = DateTime.FromBinary(long.Parse(dt,NumberStyles.AllowHexSpecifier));

            //Parse SRC
            for (int i = 0; i < srcBytes.Length; i++)
            {
                srcBytes[i] = entryBytes[21 + i];
            }
            log.Source = Encoding.UTF8.GetString(srcBytes);

            //Parse Detail
            for (int i = 0; i < detailBytes.Length; i++)
            {
                detailBytes[i] = entryBytes[21 + srcBytes.Length + i];
            }
            log.Detail = Encoding.UTF8.GetString(detailBytes);

            return log;
        }
    }
}
