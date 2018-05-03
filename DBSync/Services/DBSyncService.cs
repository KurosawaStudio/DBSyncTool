using DBSync.Enumerations;
using DBSync.Ini;
using DBSync.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DBSync.LogicClasses;
using DBSync.Properties;
using STT = System.Timers.Timer;

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
        private readonly Image nullImage=new Bitmap(16,16);

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
            new Thread(() =>
            {
                //线程操作开始
                STT timer = new STT();
                timer.Interval = 60000;
                timer.Elapsed += (sender, e) =>
                {
                    //启动线程操作
                    InitAndDoSyncWork();
                };
                
                timer.AutoReset = true;
                timer.Enabled = true;
            }).Start();
            
        }

        private void dgv_UpdateProgressToggles(int row, int column, object value, string plan, string step)
        {
            //dgv.Rows[row].Cells[column].Value = value;
            string title = $@"同步线程 - {plan}";
            string msg = $"任务计划: {plan}\r\n计划步骤:{step}\r\n消息内容:\r\n{value}";
            LogManager.CreateLogManager().AddLog(title, LogLevel.Info, msg);
        }

        private void Invoke(Delegate evt, int cur, int column, object obj, string plan, string step)
        {
            evt.DynamicInvoke(cur, column, obj, plan, step);
        }

        private delegate void UpdateProgressToggles(int row, int column, object value, string plan, string step);

        private string GetSql(string Source, string fileName)
        {
            string SqlBasePath = $@"{Program.basePath}\SQL\{Source}\";
            string Sql = File.ReadAllText($@"{SqlBasePath}{fileName}.sql");
            return Sql;
        }

        private void InitAndDoSyncWork()
        {
            //初始化并开始同步
            Delegate dgvEvt=new UpdateProgressToggles(dgv_UpdateProgressToggles);
            string smsg, dmsg;
            int ok = 0, fail = 0, miss = 0, index = 0, total = 0;
            int cur = 0;

            foreach (PlanData PlanData in PlanHelper.Create().GetPlanData((plan) =>
            {
                bool ret = true;
                bool onetime =
                    //执行一次的任务判断
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm").Equals($@"{plan.PlanDate:yyyy-MM-dd} {plan.PlanTime:HH:mm}") && plan.PlanDateModel == PlanDateModel.执行一次;

                bool repeat = //重复执行的判断
                    plan.PlanDateModel == PlanDateModel.重复执行;

                bool everyday = (DateTime.Now.Date - plan.LastSuccessTime).Days == plan.PlanDayStep;
                everyday &= plan.PlanTimeModel == PlanTimeModel.每天;

                bool everyweek = (DateTime.Now.Date - plan.LastSuccessTime).Days / 7 == plan.PlanDayStep;
                everyweek &= plan.PlanWeek.ToString()
                    .Contains((DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int) DateTime.Now.Date.DayOfWeek)
                        .ToString());
                everyweek &= plan.PlanTimeModel == PlanTimeModel.每周;

                bool time = (int) ((DateTime.Now.TimeOfDay - plan.LastSuccessTime.TimeOfDay).TotalMinutes) >=
                            plan.PlanTimeStep;

                ret = onetime || (repeat && (everyday || everyweek) && time);
                ret &= (!plan.Working);
                return ret;
                    

            }))
            {
                try
                {
                    PlanData.Working=true;
                    PlanHelper.Create().UpdatePlanData(PlanData);
                    //Invoke(dgvEvt, cur, 0, Resources.busy2);
                    Invoke(dgvEvt, cur, 2, "正在获取数据库连接", "任务初始化", "数据库连接");
                    GetDBConfigForService();
                    Thread.Sleep(1000);
                    Invoke(dgvEvt, cur, 2, "正在测试数据库连接", "任务初始化", "数据库连接");
                    TryDBConfigForService();
                    Thread.Sleep(1000);
                    //Invoke(dgvEvt, cur, 0, Resources.success);
                    Invoke(dgvEvt, cur, 2, "完成", "任务初始化", "数据库连接");

                    List<PlanDataItem> piis = PlanHelper.Create().GetPlanItemDataOrdered(PlanData.ID);
                    foreach (PlanDataItem pii in piis)
                    {
                        for (cur = 1; cur < 5; cur++)
                        {
                            //Invoke(dgvEvt, cur, 0, nullImage);
                            //Invoke(dgvEvt, cur, 2, "");
                        }

                        Thread.Sleep(100);

                        cur = 1;
                        //Invoke(dgvEvt, cur, 0, Resources.busy2);
                        Invoke(dgvEvt, cur, 2, $@"正在获取""{pii.PlanDataName}""的同步设置", PlanData.Name, $"{pii.PlanDataName} - 正在准备同步");
                        string srcSql = GetSql("Src", pii.PlanSql);
                        string dstSql = GetSql("Dst", pii.PlanSql);
                        Thread.Sleep(1000);
                        //Invoke(dgvEvt, cur, 0, Resources.success);
                        Invoke(dgvEvt, cur, 2, $@"""{pii.PlanDataName}"" - 准备就绪",  PlanData.Name, $"{pii.PlanDataName} - 正在开始步骤");

                        cur = 2;
                        //Invoke(dgvEvt, cur, 0, Resources.busy2);
                        Invoke(dgvEvt, cur, 2, "正在获取数据", PlanData.Name, $"{pii.PlanDataName} - 正在获取数据");
                        DataSet ds = SQLHelper.QueryDataSet(Src, srcSql, null, out smsg);
                        if (ds.Tables.Count == 0)
                        {
                            throw new Exception($@"数据库查询'{srcSql}'没有返回表");
                        }

                        //Invoke(dgvEvt, cur, 0, Resources.success);
                        total = ds.Tables[0].Rows.Count;
                        Invoke(dgvEvt, cur, 2, $@"发现{total}条数据", PlanData.Name, $"{pii.PlanDataName} - 正在获取数据");

                        cur = 3;
                        ok = 0;
                        fail = 0;
                        miss = 0;
                        //Invoke(dgvEvt, cur, 0, Resources.busy2);
                        index = 0;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            index++;
                            Invoke(dgvEvt, cur, 2, $@"正在复制第{index}条(共{total}条)", PlanData.Name, $"{pii.PlanDataName} - 正在获取数据");
                            List<IDbDataParameter> paras = new List<IDbDataParameter>();
                            foreach (DataColumn col in ds.Tables[0].Columns)
                            {
                                IDbDataParameter para =
                                    SQLHelper.CreateParameter(Dest, "@" + col.ColumnName, dr[col], out dmsg);
                                paras.Add(para);
                            }

                            try
                            {
                                SQLHelper.NonQuery(Dest, dstSql, paras, out dmsg);
                                ok++;
                            }
                            catch (Exception ex)
                            {
                                string msg = ex.Message;
                                Regex skipRegex = new Regex("^(SKIP).*$");

                                if (skipRegex.IsMatch(msg))
                                {
                                    miss++;
                                }
                                else
                                {
                                    if (ex is SqlException && (ex as SqlException).Class == 18)
                                    {
                                        fail++;
                                    }
                                    else
                                    {
                                        if (pii.FailMode == FailMode.退出执行)
                                        {
                                            throw;
                                        }
                                    }
                                }

                                File.AppendAllText(pii.PlanDataName + ".txt", msg + Environment.NewLine);
                            }
                        }

                        //Invoke(dgvEvt, cur, 0, Resources.success);
                        Invoke(dgvEvt, cur, 2, $@"复制成功{ok}条,失败{fail}条,忽略{miss}条", PlanData.Name, $"{pii.PlanDataName} - 正在获取数据");

                        Thread.Sleep(1000);
                        cur = 4;
                        //Invoke(dgvEvt, cur, 0, Resources.busy2);
                        Invoke(dgvEvt, cur, 2, "进行中", PlanData.Name, $"{pii.PlanDataName} - 正在结束步骤");
                        Thread.Sleep(1000);
                        //Invoke(dgvEvt, cur, 0, Resources.success);
                        Invoke(dgvEvt, cur, 2, $@"复制成功{ok}条,失败{fail}条,忽略{miss}条", PlanData.Name, $"{pii.PlanDataName} - 正在结束步骤");
                        Thread.Sleep(1000);
                        cur = 1;
                    }

                    cur = 5;
                    //Invoke(dgvEvt, cur, 0, Resources.busy2);
                    Invoke(dgvEvt, cur, 2, "进行中", PlanData.Name, "正在结束同步");
                    Thread.Sleep(1000);
                    //Invoke(dgvEvt, cur, 0, Resources.success);
                    Invoke(dgvEvt, cur, 2, "完成", PlanData.Name, "正在结束同步");
                    PlanData.LastSuccessTime=DateTime.Now;
                    PlanData.Working=false;
                    PlanHelper.Create().UpdatePlanData(PlanData);
                }
                catch (Exception ex)
                {
                    //Message = ex.Message;
                    //Invoke(new btnMessageShow(()=>btnMessage.Visible=true), null);
                    //Invoke(dgvEvt, cur, 0, Resources.fail);
                    Invoke(dgvEvt, cur, 2, "失败" + "\r\n" + ex.Message, PlanData.Name, "出错信息");
                }

                //timer.Stop();
            }
        }
    }
}
