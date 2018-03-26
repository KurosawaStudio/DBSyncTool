using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBSync.Enumerations;
using DBSync.Ini;
using DBSync.Log;
using DBSync.LogicClasses;
using DBSync.Properties;
using DBSync.Services;
using MySql.Data.MySqlClient;

namespace DBSync.Logics
{
    public partial class frmSync : Form
    {
        TimeSpan time=new TimeSpan(0,0,0);
        public PlanData PlanData;
        private Thread tSync;
        private Image nullImage=new Bitmap(16,16);
        private bool error = false;
        private DatabaseConfig Dest,Src;
        private string Message = "";
        int cur = 0;

        public frmSync()
        {
            InitializeComponent();
            InitializeProgressView();
            InitializeTimeSpan();
        }

        #region Initialize
        private void InitializeProgressView()
        {
            dgv.Rows.Add(nullImage, "正在准备同步", "");
            dgv.Rows.Add(nullImage, "正在开始步骤", "");
            dgv.Rows.Add(nullImage, "正在获取数据", "");
            dgv.Rows.Add(nullImage, "正在发送数据", "");
            dgv.Rows.Add(nullImage, "正在结束步骤", "");
            dgv.Rows.Add(nullImage, "正在结束同步", "");
            dgv.ClearSelection();
        }

        private void InitializeTimeSpan()
        {
            time=new TimeSpan(0,0,0);
            lblTime.Text = @"00:00";
        }

        private void InitializeThread()
        {
            tSync=new Thread(DoSyncWork);
        }
        #endregion

        #region Events
        private void timer_Tick(object sender, EventArgs e)
        {
            time=time.Add(new TimeSpan(0, 0, 1));
            lblTime.Text = time.Hours>0?time.ToString(@"hh\:mm\:ss"):time.ToString(@"mm\:ss");
        }

        private void frmSync_Load(object sender, EventArgs e)
        {
            InitializeThread();
            timer.Start();
            tSync.Start();
            
        }
        
        private void btnMessage_Click(object sender, EventArgs e)
        {
            if (Message != "")
            {
                MessageBox.Show(Message, @"计划任务", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex==cur)
            {
                btnMessage_Click(sender,e);
            }
        }

        private void dgv_UpdateProgressToggles(int row, int column, object value)
        {
            dgv.Rows[row].Cells[column].Value = value;
        }

        #endregion

        #region Delegates
        private delegate void UpdateProgressToggles(int row, int column, object value);

        private delegate void btnMessageShow();
        #endregion

        private void GetDBConfigForService()
        {
            #region Try Get Config File
            string configFile = $@"{Program.basePath}\Sync.ini";
            //LogManager.CreateLogManager().AddLog("配置文件",LogLevel.Info,$@"正在检测配置文件{configFile} ...");
            if (!File.Exists(configFile))
            {
                throw new Exception("配置文件丢失，服务将自动停止");
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
                //LogManager.CreateLogManager().AddLog("配置文件", LogLevel.Info, $"配置文件读取成功");
            }
            catch (Exception ex)
            {
                throw new Exception($"配置文件读取错误，服务将自动停止\r\n错误类型:{ex.GetType().FullName}({ex.Message})\r\n错误堆栈:{ex.StackTrace}");
                
            }
            #endregion

        }

        private void TryDBConfigForService()
        {
            //LogManager.CreateLogManager().AddLog("数据库测试", LogLevel.Info, $"正在测试源数据库连接");
            if (!SQLHelper.TryConnection(Src, out string smsg))
            {
                throw new Exception($"源数据库连接失败,服务自动停止\r\n错误信息:{smsg}");
            }
            //LogManager.CreateLogManager().AddLog("数据库测试", LogLevel.Info, $"源数据库连接成功");
            //LogManager.CreateLogManager().AddLog("数据库测试", LogLevel.Info, $"正在测试目标数据库连接");
            if (!SQLHelper.TryConnection(Dest, out string dmsg))
            {
                throw new Exception($"目标数据库连接失败,服务自动停止\r\n错误信息:{dmsg}");
                
            }
            //LogManager.CreateLogManager().AddLog("配置文件", LogLevel.Info, $"目标数据库连接成功");
        }

        private string GetSql(string Source, string fileName)
        {
            string SqlBasePath = $@"{Program.basePath}\SQL\{Source}\";
            string Sql = File.ReadAllText($@"{SqlBasePath}{fileName}.sql");
            return Sql;
        }

        private void DoSyncWork()
        {
            Delegate dgvEvt=new UpdateProgressToggles(dgv_UpdateProgressToggles);
            string smsg, dmsg;
            int ok = 0, fail = 0, miss = 0, index = 0, total = 0;
            
            try
            {
                Invoke(dgvEvt, cur, 0, Resources.busy2);
                Invoke(dgvEvt, cur, 2, "正在获取数据库连接");
                GetDBConfigForService();
                Thread.Sleep(1000);
                Invoke(dgvEvt, cur, 2, "正在测试数据库连接");
                TryDBConfigForService();
                Thread.Sleep(1000);
                Invoke(dgvEvt, cur, 0, Resources.success);
                Invoke(dgvEvt, cur, 2, "完成");

                List<PlanDataItem> piis = PlanHelper.Create().GetPlanItemDataOrdered(PlanData.ID);
                foreach (PlanDataItem pii in piis)
                {
                    for (cur=1;cur<5;cur++)
                    {
                        Invoke(dgvEvt, cur, 0, nullImage);
                        Invoke(dgvEvt, cur, 2, "");
                    }
                    Thread.Sleep(100);

                    cur = 1;
                    Invoke(dgvEvt, cur, 0, Resources.busy2);
                    Invoke(dgvEvt, cur, 2, $@"正在获取""{pii.PlanDataName}""的同步设置");
                    string srcSql=GetSql("Src",pii.PlanSql);
                    string dstSql=GetSql("Dst",pii.PlanSql);
                    Thread.Sleep(1000);
                    Invoke(dgvEvt, cur, 0, Resources.success);
                    Invoke(dgvEvt, cur, 2, $@"""{pii.PlanDataName}"" - 准备就绪");

                    cur = 2;
                    Invoke(dgvEvt, cur, 0, Resources.busy2);
                    Invoke(dgvEvt, cur, 2, "正在获取数据");
                    DataSet ds = SQLHelper.QueryDataSet(Src, srcSql, null, out smsg);
                    if (ds.Tables.Count == 0)
                    {
                        throw new Exception($@"数据库查询'{srcSql}'没有返回表");
                    }
                    Invoke(dgvEvt, cur, 0, Resources.success);
                    total = ds.Tables[0].Rows.Count;
                    Invoke(dgvEvt, cur, 2, $@"发现{total}条数据");

                    cur = 3;
                    ok = 0;
                    fail = 0;
                    Invoke(dgvEvt, cur, 0, Resources.busy2);
                    index = 0;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        index++;
                        Invoke(dgvEvt, cur, 2, $@"正在复制第{index}条(共{total}条)");
                        List<IDbDataParameter> paras = new List<IDbDataParameter>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            IDbDataParameter para = SQLHelper.CreateParameter(Dest,"@" + col.ColumnName, dr[col],out dmsg);
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
                            Regex skipRegex=new Regex("^(SKIP).*$");

                            if (skipRegex.IsMatch(msg))
                            {
                                miss++;
                            }
                            else
                            {
                                fail++;
                                if (pii.FailMode == FailMode.退出执行)
                                {
                                    throw ex;
                                }
                            }
                        }
                    }
                    
                    Invoke(dgvEvt, cur, 0, Resources.success);
                    Invoke(dgvEvt, cur, 2, $@"复制成功{ok}条,失败{fail}条,忽略{miss}条");

                    Thread.Sleep(1000);
                    cur = 4;
                    Invoke(dgvEvt, cur, 0, Resources.busy2);
                    Invoke(dgvEvt, cur, 2, "进行中");
                    Thread.Sleep(1000);
                    Invoke(dgvEvt, cur, 0, Resources.success);
                    Invoke(dgvEvt, cur, 2, $@"复制成功{ok}条,失败{fail}条,忽略{miss}条");
                    Thread.Sleep(1000);
                    cur = 1;
                }

                cur = 5;
                Invoke(dgvEvt, cur, 0, Resources.busy2);
                Invoke(dgvEvt, cur, 2, "进行中");
                Thread.Sleep(1000);
                Invoke(dgvEvt, cur, 0, Resources.success);
                Invoke(dgvEvt, cur, 2, "完成");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Invoke(new btnMessageShow(()=>btnMessage.Visible=true), null);
                Invoke(dgvEvt, cur, 0, Resources.fail);
                Invoke(dgvEvt, cur, 2, "失败");
            }
            timer.Stop();
        }

       

    }
}
