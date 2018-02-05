using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DBSync.Enumerations;
using DBSync.Ini;
using DBSync.Log;
using STT=System.Timers.Timer;

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
        private Thread worker;

        protected override void OnStart(string[] args)
        {
            GetParametersForService();
            GetDBConfigForService();
            TryDBConfigForService();
            WriteSuccessLogForService();
            StartNewThreadForService();
        }
        
        #region Basic Service Functions

        private void WriteSuccessLogForService()
        {
            LogManager.CreateLogManager().AddLog("系统服务运行池",LogLevel.Info,"服务启动成功");
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

        #endregion

        private void StartNewThreadForService()
        {
            LogManager.CreateLogManager().AddLog("线程操作池",LogLevel.Info,"开启线程操作");
            try
            {
                new Thread(() =>
                {
                    Mutex mutex = new Mutex(true, "run");
                    bool mutexed=mutex.WaitOne(60000);
                    if (!mutexed)
                    {
                        LogManager.CreateLogManager().AddLog("线程操作池", LogLevel.Info, "线程启动失败\r\n错误原因：上一个线程尚未退出");
                        return;
                    }
                    WorkerThreadForService();
                    mutex.ReleaseMutex();
                }).Start();
                LogManager.CreateLogManager().AddLog("线程操作池", LogLevel.Info, "线程启动成功");
            }
            catch (Exception ex)
            {
                LogManager.CreateLogManager().AddLog("线程操作池", LogLevel.Error, $@"线程启动失败,错误类型:{ex.GetType().FullName}\r\n错误信息:{ex.Message}\r\n错误堆栈:{ex.StackTrace}");
            }
            
        }

        private void WorkerThreadForService()
        {
            string SrcSqlBasePath = $@"{Program.basePath}\SQL\Src\";
            string DstSqlBasePath = $@"{Program.basePath}\SQL\Dst\";

            DirectoryInfo srcDirInfo=new DirectoryInfo(SrcSqlBasePath);
            DirectoryInfo dstDirInfo=new DirectoryInfo(DstSqlBasePath);
            SortedList<string,string> srcSqlList=new SortedList<string,string>();
            SortedList<string,string> dstSqlList=new SortedList<string,string>();

            FileInfo[] sfis = srcDirInfo.GetFiles("product.sql");
            foreach (var sfi in sfis)
            {
                srcSqlList.Add(sfi.Name,sfi.FullName);
            }

            FileInfo[] dfis = dstDirInfo.GetFiles("product.sql");
            foreach (var dfi in dfis)
            {
                dstSqlList.Add(dfi.Name,dfi.FullName);
            }
            
            STT timer = new STT(60000);
            timer.Elapsed += (sender, e) =>
            {
                Mutex mutex = new Mutex(true, "sync");
                bool mutexed = mutex.WaitOne(60000);
                if (!mutexed)
                {
                    LogManager.CreateLogManager().AddLog("同步线程", LogLevel.Info, "线程同步失败\r\n错误原因：上一个线程尚未退出");
                    return;
                }
                DoSqlForService(srcSqlList, dstSqlList);
                //mutex.Close();
                mutex.ReleaseMutex();
            };
            timer.Start();
        }        

        private void DoSqlForService(SortedList<string, string> srcSqlList, SortedList<string, string> dstSqlList)
        {
            foreach (var key in srcSqlList.Keys)
            {
                string srcSql = File.ReadAllText(srcSqlList[key]);
                string smsg, dmsg;
                DataSet ds = SQLHelper.QueryDataSet(Src, srcSql, null, out smsg);
                ds.WriteXml("ds.xml");

                if (ds.Tables.Count == 0)
                {
                    LogManager.CreateLogManager().AddLog("数据库查询",LogLevel.Error,$@"数据库查询'{srcSql}'没有返回表");
                    continue;
                }

                if (!dstSqlList.ContainsKey(key))
                {
                    LogManager.CreateLogManager().AddLog("数据库查询", LogLevel.Error, $@"数据库查询'{key}'没有对应的同步查询");
                    continue;
                }

                string dstSql = File.ReadAllText(dstSqlList[key]);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    List<IDbDataParameter> paras = new List<IDbDataParameter>();
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        IDbDataParameter para = SQLHelper.CreateParameter(Dest,"@" + col.ColumnName, dr[col],out dmsg);
                        paras.Add(para);
                    }
                    SQLHelper.NonQuery(Dest, dstSql, paras, out dmsg);
                }
                
            }
        }
    }
}
