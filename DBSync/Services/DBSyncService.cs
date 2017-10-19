using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBSync.Enumerations;
using DBSync.Ini;
using DBSync.Log;

namespace DBSync.Services
{
    partial class DBSyncService : ServiceBase
    {
        public DBSyncService()
        {
            InitializeComponent();
        }
        
        private bool error = false;
        private DatabaseConfig Dest,Src;

        protected override void OnStart(string[] args)
        {
            GetParametersForService();
            GetDBConfigForService();
        }

        private void GetDBConfigForService()
        {
            #region Try Get Config File
            string configFile = $@"{Program.basePath}\Sync.ini";
            LogManager.CreateLogManager().AddLog("配置文件",LogLevel.Info,$@"正在检测配置文件{configFile} ...");
            if (!File.Exists(configFile))
            {
                LogManager.CreateLogManager().AddLog("配置文件",LogLevel.Error,"配置文件丢失，服务将自动停止");
                error = true;
                Stop();
            }
            #endregion

            #region Try Parse Config File
            
            try
            {
                IniFile ini = IniFile.FromIni(File.ReadAllLines("Sync.ini"));
                Dest = new DatabaseConfig
                {
                    ServerType =
                        (DatabaseServerType) Enum.Parse(typeof(DatabaseServerType), ini["SourceDatabase"]["Type"]),
                    DataSource = ini["SourceDatabase"]["DataSource"],
                    Port = int.Parse(ini["SourceDatabase"]["Port"]),
                    UserID = ini["SourceDatabase"]["UserID"],
                    Password = ini["SourceDatabase"]["Password"],
                    AuthType = (DatabaseAuthType) Enum.Parse(typeof(DatabaseAuthType),
                        ini["SourceDatabase"]["AuthType"]),
                    Database = ini["SourceDatabase"]["Database"]
                };

                Src = new DatabaseConfig();
                Src.ServerType =
                    (DatabaseServerType) Enum.Parse(typeof(DatabaseServerType), ini["DestDatabase"]["Type"]);
                Src.DataSource = ini["DestDatabase"]["DataSource"];
                Src.Port = int.Parse(ini["DestDatabase"]["Port"]);
                Src.UserID = ini["DestDatabase"]["UserID"];
                Src.Password = ini["DestDatabase"]["Password"];
                Src.AuthType = (DatabaseAuthType) Enum.Parse(typeof(DatabaseAuthType), ini["DestDatabase"]["AuthType"]);
                Src.Database = ini["DestDatabase"]["Database"];
            }
            catch (Exception ex)
            {
                LogManager.CreateLogManager().AddLog("配置文件", LogLevel.Error, $"配置文件读取错误，服务将自动停止\r\n错误类型:{ex.GetType().FullName}({ex.Message})\r\n错误堆栈:{ex.StackTrace}");
                error = true;
                Stop();
            }
            #endregion

        }

        private void GetParametersForService()
        {
            Program.exePath = Application.ExecutablePath;
            Program.basePath = Application.StartupPath;
            LogManager logManager=LogManager.CreateLogManager();
            logManager.AddLog("系统服务运行池",LogLevel.Info,$"程序启动路径:{Program.exePath}\r\n启动基础路径:{Program.basePath}");
            
        }

        protected override void OnStop()
        {
            LogManager.CreateLogManager().AddLog("系统服务运行池", LogLevel.Info, "服务已停止");
        }
    }
}
