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
            TryDBConfigForService();
        }

        private void TryDBConfigForService()
        {
            LogManager.CreateLogManager().AddLog("数据库测试", LogLevel.Info, $"正在测试源数据库连接");
            if (!SQLHelper.TryConnection(Src, out string smsg))
            {
                LogManager.CreateLogManager().AddLog("数据库测试", LogLevel.Info, $"源数据库连接失败,服务自动停止\r\n错误信息:{smsg}");
                error = true;
                Stop();
                return;
            }
            LogManager.CreateLogManager().AddLog("数据库测试", LogLevel.Info, $"源数据库连接成功");
            LogManager.CreateLogManager().AddLog("数据库测试", LogLevel.Info, $"正在测试目标数据库连接");
            if (!SQLHelper.TryConnection(Dest, out string dmsg))
            {
                LogManager.CreateLogManager().AddLog("数据库测试", LogLevel.Info, $"目标数据库连接失败,服务自动停止\r\n错误信息:{dmsg}");
                error = true;
                Stop();
                return;
            }
            LogManager.CreateLogManager().AddLog("配置文件", LogLevel.Info, $"目标数据库连接成功");
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
                IniFile ini = IniFile.FromIni(File.ReadAllLines($@"{Program.basePath}\Sync.ini"));
                Src = new DatabaseConfig
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

                Dest = new DatabaseConfig
                {
                    ServerType =
                    (DatabaseServerType)Enum.Parse(typeof(DatabaseServerType), ini["DestDatabase"]["Type"]),
                    DataSource = ini["DestDatabase"]["DataSource"],
                    Port = int.Parse(ini["DestDatabase"]["Port"]),
                    UserID = ini["DestDatabase"]["UserID"],
                    Password = ini["DestDatabase"]["Password"],
                    AuthType = (DatabaseAuthType)Enum.Parse(typeof(DatabaseAuthType), ini["DestDatabase"]["AuthType"]),
                    Database = ini["DestDatabase"]["Database"]
                };
                LogManager.CreateLogManager().AddLog("配置文件", LogLevel.Info, $"配置文件读取成功");
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
