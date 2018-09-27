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
        private readonly string _log = $@"{Program.basePath}\log\{DateTime.Now:yyyy-MM-dd-HH}.log";
        private LogFile _logFile=new LogFile();
        private LogManager()
        {
            if (Directory.Exists($@"{Program.basePath}\log\"))
            {
                Directory.CreateDirectory($@"{Program.basePath}\log\");
            }
            if (File.Exists(_log))
            {
                _logFile = LogFile.FromLogBytes(File.ReadAllBytes(_log));
            }
            else
            {
                _logFile=new LogFile();
            }
        }

        public static LogManager CreateLogManager() => new LogManager();


        public EntryAddedHandler OnEntryAdded { get; set; }

        public void AddLog(string Source,LogLevel LogLevel,string Detail)
        {
            if (_logFile.entries == null)
            {
                _logFile.entries=new List<LogEntry>();
            }
            LogEntry entry = new LogEntry
            {
                Source = Source,
                LogLevel = LogLevel,
                LogTime = DateTime.Now,
                Detail = Detail
            };
            _logFile.entries.Add(entry);
            File.WriteAllBytes(_log,_logFile.ToLogBytes());
            OnEntryAdded?.Invoke(_log);
        }

        public List<LogEntry> GetEntries()
        {
            return _logFile.entries;
        }
    }

    public delegate void EntryAddedHandler(string logFile);
}
