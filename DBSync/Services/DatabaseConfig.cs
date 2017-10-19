using DBSync.Enumerations;

namespace DBSync.Services
{
    public class DatabaseConfig
    {
        public DatabaseServerType ServerType { get; internal set; }
        public string DataSource { get; internal set; }
        public int Port { get; internal set; }
        public string UserID { get; internal set; }
        public string Password { get; internal set; }
        public DatabaseAuthType AuthType { get; internal set; }
        public string Database { get; internal set; }
    }
}