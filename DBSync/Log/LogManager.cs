using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSync.Log
{
    public class LogManager
    {
        private string log = $@"{Program.basePath}\log.log";
        private LogFile logFile=new LogFile();
        private LogManager()
        {
            if (File.Exists(log))
            {
                logFile = LogFile.FromLogBytes(File.ReadAllBytes(log));
            }
            else
            {
                logFile=new LogFile();
            }
        }

        public static LogManager CreateLogManager() => new LogManager();


        public EntryAddedHandler OnEntryAdded { get; set; }

        public void AddLog(string Source,LogLevel LogLevel,string Detail)
        {
            if (logFile.entries == null)
            {
                logFile.entries=new List<LogEntry>();
            }
            LogEntry entry = new LogEntry
            {
                Source = Source,
                LogLevel = LogLevel,
                LogTime = DateTime.Now,
                Detail = Detail
            };
            logFile.entries.Add(entry);
            File.WriteAllBytes(log,logFile.ToLogBytes());
            OnEntryAdded?.Invoke(log);
        }

        public List<LogEntry> GetEntries()
        {
            return logFile.entries;
        }
    }

    public delegate void EntryAddedHandler(string logFile);
}
