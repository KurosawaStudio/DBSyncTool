using DBSync.Enumerations;
using DBSync.Ini;
using DBSync.Log;
using DBSync.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DBSync.LogicClasses;

namespace DBSync.Logics
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        public frmMain(bool debugMode)
        {
            InitializeComponent();
            this.debugMode = debugMode;
        }

        private bool debugMode = false;

        private void frmMain_Load(object sender, EventArgs e)
        {
            gridLog.AutoGenerateColumns = false;
            dgvSqlSteps.AutoGenerateColumns = false;
            dbgrdconfig.AutoGenerateColumns = false;
            InitTabLog();
            InitTabSrc();
            InitTabDst();
            InitTabSql();
            InitDebugMode();
            InitConfigDB();
            InitPlanPanel();
            LoadPlanData();
            mainTab.SelectedIndex = mainTab.TabPages.IndexOf(tLog);
        }

        private void InitConfigDB()
        {
            cdsConfig.ReadXml($@"{Program.basePath}\config.xml");
            dbgrdconfig.Update();
            dbgrdconfig.AllowUserToAddRows = true;
        }
       
        private void InitDebugMode()
        {
            if (!debugMode)
            {
                tSQLTest.Visible = false;
                rbSQLDst.Visible = false;
                tTarget.Visible = false;
                mainTab.TabPages.Remove(tSQLTest);
                mainTab.TabPages.Remove(tTarget);
                txtNewFile.Visible = false;
                btnNewFile.Visible = false;
                btnDelete.Visible = false;
            }
        }

        #region Tab Log

        private List<LogEntry> entries;

        private void InitTabLog()
        {
            btnRLog_Click(this, new EventArgs());
        }

        private void gridLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                txtLog.Text = entries[e.RowIndex].Detail;
            }
        }

        private void btnRLog_Click(object sender, EventArgs e)
        {
            LogManager logManager=LogManager.CreateLogManager();
            entries = logManager.GetEntries();
            gridLog.DataSource = entries;
            gridLog.Update();
        }
        #endregion

        #region Tab Src

        private IniFile ini = null;

        private void InitTabSrc()
        {
            ini = IniFile.FromIni(File.ReadAllLines("Sync.ini"));
            cbSrcType.SelectedIndex = (int) Enum.Parse(typeof(DatabaseServerType), ini["SourceDatabase"]["Type"]);
            txtSrcSource.Text = ini["SourceDatabase"]["DataSource"];
            txtSrcPort.Text = ini["SourceDatabase"]["Port"];
            txtSrcUser.Text = ini["SourceDatabase"]["UserID"];
            txtSrcPassword.Text = ini["SourceDatabase"]["Password"];
            cbSrcAuthz.SelectedIndex = (int) Enum.Parse(typeof(DatabaseAuthType), ini["SourceDatabase"]["AuthType"]);
            txtSrcDB.Text = ini["SourceDatabase"]["Database"];
        }

        private void btnSrcApply_Click(object sender, EventArgs e)
        {
            if (ini == null)
            {
                ini = new IniFile();
            }

            ini["SourceDatabase"]["Type"] = ((DatabaseServerType) cbSrcType.SelectedIndex).ToString();
            ini["SourceDatabase"]["DataSource"] = txtSrcSource.Text;
            ini["SourceDatabase"]["Port"] = txtSrcPort.Text;
            ini["SourceDatabase"]["UserID"] = txtSrcUser.Text;
            ini["SourceDatabase"]["Password"] = txtSrcPassword.Text;
            ini["SourceDatabase"]["AuthType"] = ((DatabaseAuthType) cbSrcAuthz.SelectedIndex).ToString();
            ini["SourceDatabase"]["Database"] = txtSrcDB.Text;

            string data = ini.ToIni();
            File.WriteAllText("Sync.ini", data, Encoding.UTF8);
        }

        private void btnSrcTest_Click(object sender, EventArgs e)
        {
            if (ini == null)
            {
                ini = new IniFile();
            }

            ini["SourceDatabase"]["Type"] = ((DatabaseServerType)cbSrcType.SelectedIndex).ToString();
            ini["SourceDatabase"]["DataSource"] = txtSrcSource.Text;
            ini["SourceDatabase"]["Port"] = txtSrcPort.Text;
            ini["SourceDatabase"]["UserID"] = txtSrcUser.Text;
            ini["SourceDatabase"]["Password"] = txtSrcPassword.Text;
            ini["SourceDatabase"]["AuthType"] = ((DatabaseAuthType)cbSrcAuthz.SelectedIndex).ToString();
            ini["SourceDatabase"]["Database"] = txtSrcDB.Text;

            DatabaseConfig src = new DatabaseConfig
            {
                ServerType =
                        (DatabaseServerType)Enum.Parse(typeof(DatabaseServerType), ini["SourceDatabase"]["Type"]),
                DataSource = ini["SourceDatabase"]["DataSource"],
                Port = int.Parse(ini["SourceDatabase"]["Port"]),
                UserID = ini["SourceDatabase"]["UserID"],
                Password = ini["SourceDatabase"]["Password"],
                AuthType = (DatabaseAuthType)Enum.Parse(typeof(DatabaseAuthType),
                        ini["SourceDatabase"]["AuthType"]),
                Database = ini["SourceDatabase"]["Database"]
            };

            SQLHelper.TryConnection(src, out string msg);
            MessageBox.Show(msg);
        }

        private void cbSrcAuthz_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DatabaseAuthType)cbSrcAuthz.SelectedIndex) == DatabaseAuthType.Windows)
            {
                txtSrcUser.Enabled = false;
                txtSrcPassword.Enabled = false;
                txtSrcUser.ReadOnly = true;
                txtSrcPassword.ReadOnly = true;
            }
            else
            {
                txtSrcUser.Enabled = true;
                txtSrcPassword.Enabled = true;
                txtSrcUser.ReadOnly = false;
                txtSrcPassword.ReadOnly = false;
            }
        }

        #endregion

        #region Tab Dst

        private void InitTabDst()
        {
            ini = IniFile.FromIni(File.ReadAllLines("Sync.ini"));
            cbDstType.SelectedIndex = (int) Enum.Parse(typeof(DatabaseServerType), ini["DestDatabase"]["Type"]);
            txtDstSource.Text = ini["DestDatabase"]["DataSource"];
            txtDstPort.Text = ini["DestDatabase"]["Port"];
            txtDstUser.Text = ini["DestDatabase"]["UserID"];
            txtDstPassword.Text = ini["DestDatabase"]["Password"];
            cbDstAuthz.SelectedIndex = (int) Enum.Parse(typeof(DatabaseAuthType), ini["DestDatabase"]["AuthType"]);
            txtDstDB.Text = ini["DestDatabase"]["Database"];
        }

        private void btnDstApply_Click(object sender, EventArgs e)
        {
            if (ini == null)
            {
                ini = new IniFile();
            }

            ini["DestDatabase"]["Type"] = ((DatabaseServerType) cbDstType.SelectedIndex).ToString();
            ini["DestDatabase"]["DataSource"] = txtDstSource.Text;
            ini["DestDatabase"]["Port"] = txtDstPort.Text;
            ini["DestDatabase"]["UserID"] = txtDstUser.Text;
            ini["DestDatabase"]["Password"] = txtDstPassword.Text;
            ini["DestDatabase"]["AuthType"] = ((DatabaseAuthType) cbDstAuthz.SelectedIndex).ToString();
            ini["DestDatabase"]["Database"] = txtDstDB.Text;

            string data = ini.ToIni();
            File.WriteAllText("Sync.ini", data, Encoding.UTF8);
        }
        private void btnDstTest_Click(object sender, EventArgs e)
        {
            if (ini == null)
            {
                ini = new IniFile();
            }

            ini["DestDatabase"]["Type"] = ((DatabaseServerType)cbDstType.SelectedIndex).ToString();
            ini["DestDatabase"]["DataSource"] = txtDstSource.Text;
            ini["DestDatabase"]["Port"] = txtDstPort.Text;
            ini["DestDatabase"]["UserID"] = txtDstUser.Text;
            ini["DestDatabase"]["Password"] = txtDstPassword.Text;
            ini["DestDatabase"]["AuthType"] = ((DatabaseAuthType)cbDstAuthz.SelectedIndex).ToString();
            ini["DestDatabase"]["Database"] = txtDstDB.Text;

            DatabaseConfig dst = new DatabaseConfig
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

            SQLHelper.TryConnection(dst, out string msg);
            MessageBox.Show(msg);
        }
        private void cbDstAuthz_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DatabaseAuthType)cbDstAuthz.SelectedIndex) == DatabaseAuthType.Windows)
            {
                txtDstUser.Enabled = false;
                txtDstPassword.Enabled = false;
                txtDstUser.ReadOnly = true;
                txtDstPassword.ReadOnly = true;
            }
            else
            {
                txtDstUser.Enabled = true;
                txtDstPassword.Enabled = true;
                txtDstUser.ReadOnly = false;
                txtDstPassword.ReadOnly = false;
            }
        }

        #endregion

        #region Tab Test

        DataSet ds = new DataSet();
        private List<DataTable> tables;

        private void btnTestExec_Click(object sender, EventArgs e)
        {
            IDbConnection con = GetDBConnection();
            con.Open();
            IDbCommand cmd = GetDBCommand(con);
            cmd.CommandText = txtTestSQL.Text;
            cmd.CommandText = new Regex("^\\s*[gG][Oo]\\s*$", RegexOptions.Multiline).Replace(cmd.CommandText, "");
            txtMsg.Text = "";
            GetCommonParas(ref cmd);
            if (cbTestDebug.Checked)
            {
                if(con is SqlConnection)
                {
                    cmd.CommandText = "BEGIN TRAN;\r\n" + cmd.CommandText + "\r\nROLLBACK TRAN;\r\n";
                }
                else if(con is MySqlConnection)
                {
                    cmd.CommandText = "SET AUTOCOMMIT=0;\r\nBEGIN;\r\n" + cmd.CommandText + "\r\nROLLBACK;\r\nROLLBACK;\r\nSET AUTOCOMMIT=1;\r\n";
                }
            }
            try
            {
                IDataAdapter da = cmd is SqlCommand
                    ? (IDataAdapter)new SqlDataAdapter(cmd as SqlCommand)
                    : (IDataAdapter)new MySqlDataAdapter(cmd as MySqlCommand);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    gridTest.DataSource = ds.Tables[0];
                }
                else
                {
                    gridTest.DataSource = new DataTable();
                }
                gridTest.Update();
                tables = new List<DataTable>();
                foreach (DataTable dt in ds.Tables)
                {
                    tables.Add(dt);
                }
                lbGrid.DataSource = tables;
                lbGrid.Update();
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    txtMsg.Text = ex.InnerException.Message;
                }
                else
                {
                    txtMsg.Text = ex.Message;
                }
            }

            con.Close();
        }

        private void GetCommonParas(ref IDbCommand cmd)
        {
            foreach (DataRow dr in cdsConfig.Tables["global_var"].Rows)
            {
                string var_name = "gv_"+dr["var_name"].ToString();
                object var_value = dr["var_value"];
                if ((int)dr["var_type"] != 1) continue;
                if (cmd is SqlCommand)
                {
                    (cmd as SqlCommand).Parameters.AddWithValue(var_name, var_value);
                }
                else if (cmd is MySqlCommand)
                {
                    (cmd as MySqlCommand).Parameters.AddWithValue(var_name, var_value);
                }
            }
        }

        private IDbCommand GetDBCommand(IDbConnection con)
        {
            if (rbSrc.Checked)
            {
                if (cbSrcType.SelectedIndex == (int) DatabaseServerType.SqlServer)
                {
                    return new SqlCommand() {Connection = con as SqlConnection};
                }
                return new MySqlCommand() {Connection = con as MySqlConnection};
            }
            if (cbDstType.SelectedIndex == (int) DatabaseServerType.SqlServer)
            {
                return new SqlCommand() {Connection = con as SqlConnection};
            }
            return new MySqlCommand() {Connection = con as MySqlConnection};
        }

        private IDbConnection GetDBConnection()
        {
            if (rbSrc.Checked)
            {
                if (cbSrcType.SelectedIndex == (int) DatabaseServerType.SqlServer)
                {
                    SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
                    sb.DataSource = txtSrcSource.Text;
                    sb.InitialCatalog = txtSrcDB.Text;
                    if (cbSrcAuthz.SelectedIndex == (int) DatabaseAuthType.Windows)
                    {
                        sb.IntegratedSecurity = true;
                    }
                    else
                    {
                        sb.UserID = txtSrcUser.Text;
                        sb.Password = txtSrcPassword.Text;
                    }
                    SqlConnection con = new SqlConnection(sb.ConnectionString);
                    con.InfoMessage += (s, e) =>
                    {
                        txtMsg.Text += e.Message+"\r\n";
                    };
                    return con;
                }
                else
                {
                    MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
                    sb.Server = txtSrcSource.Text;
                    sb.Port = uint.Parse(txtSrcPort.Text);
                    sb.Database = txtSrcDB.Text;
                    sb.AllowBatch = true;
                    sb.AllowUserVariables = true;
                    if (cbSrcAuthz.SelectedIndex == (int) DatabaseAuthType.Windows)
                    {
                        sb.IntegratedSecurity = true;
                    }
                    else
                    {
                        sb.UserID = txtSrcUser.Text;
                        sb.Password = txtSrcPassword.Text;
                    }
                    MySqlConnection con = new MySqlConnection(sb.ConnectionString);
                    con.InfoMessage += (s, e) =>
                    {
                        foreach (var err in e.errors)
                        {
                            txtMsg.Text += err.Message + "\r\n";
                        }
                    };
                    return con;
                }
            }
            if (cbDstType.SelectedIndex == (int) DatabaseServerType.SqlServer)
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
                sb.DataSource = txtDstSource.Text;
                sb.InitialCatalog = txtDstDB.Text;
                if (cbDstAuthz.SelectedIndex == (int) DatabaseAuthType.Windows)
                {
                    sb.IntegratedSecurity = true;
                }
                else
                {
                    sb.UserID = txtDstUser.Text;
                    sb.Password = txtDstPassword.Text;
                }
                SqlConnection con = new SqlConnection(sb.ConnectionString);
                con.InfoMessage += (s, e) =>
                {
                    txtMsg.Text += e.Message+"\r\n";
                };
                return con;
            }
            else
            {
                MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
                sb.Server = txtDstSource.Text;
                sb.Port = uint.Parse(txtDstPort.Text);
                sb.Database = txtDstDB.Text;
                sb.AllowBatch = true;
                sb.AllowUserVariables = true;
                if (cbDstAuthz.SelectedIndex == (int) DatabaseAuthType.Windows)
                {
                    sb.IntegratedSecurity = true;
                }
                else
                {
                    sb.UserID = txtDstUser.Text;
                    sb.Password = txtDstPassword.Text;
                }
                MySqlConnection con = new MySqlConnection(sb.ConnectionString);
                con.InfoMessage += (s, e) =>
                {
                    foreach (var err in e.errors)
                    {
                        txtMsg.Text += err.Message + "\r\n";
                    }
                };
                return con;
            }
        }

        private void lbGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbGrid.SelectedIndex > -1)
            {
                gridTest.DataSource = ds.Tables[lbGrid.SelectedIndex];
                gridTest.Update();
            }
        }

        #endregion

        #region Tab SQL
        
        List<string> FileName = new List<string>();

        private void InitTabSql()
        {
            FileName = new List<string>();
            DirectoryInfo dis = new DirectoryInfo(@"SQL\Src");
            DirectoryInfo did = new DirectoryInfo(@"SQL\Dst");

            foreach (FileInfo di in dis.GetFiles("*.sql", SearchOption.TopDirectoryOnly))
            {
                if (!FileName.Contains(di.Name.Replace(di.Extension, "")))
                {
                    FileName.Add(di.Name.Replace(di.Extension, ""));
                }
            }

            foreach (FileInfo di in did.GetFiles("*.sql", SearchOption.TopDirectoryOnly))
            {
                if (!FileName.Contains(di.Name.Replace(di.Extension, "")))
                {
                    FileName.Add(di.Name.Replace(di.Extension, ""));
                }
            }

            cbSQLSelect.DataSource = FileName;
            cbSQLSelect.Update();
        }

        private string Path = "Src";

        private void cbSQLSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Path = "Src";
            rbSQLSrc.Checked = true;
            if (!File.Exists($@"SQL\{Path}\{FileName[cbSQLSelect.SelectedIndex]}.sql"))
            {
                Path = "Dst";
                rbSQLDst.Checked = true;
            }
            rbSQLSrc_CheckedChanged(sender, e);
        }

        private void rbSQLSrc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSQLSrc.Checked)
            {
                Path = "Src";
            }
            else
            {
                Path = "Dst";
            }
            if (File.Exists($@"SQL\{Path}\{FileName[cbSQLSelect.SelectedIndex]}.sql"))
            {
                txtSQL.Text = File.ReadAllText($@"SQL\{Path}\{FileName[cbSQLSelect.SelectedIndex]}.sql",
                    Encoding.UTF8);
            }
            else
            {
                txtSQL.Text = "";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSQL.Text = File.ReadAllText($@"SQL\{Path}\{FileName[cbSQLSelect.SelectedIndex]}.sql",
                Encoding.UTF8);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText($@"SQL\{Path}\{FileName[cbSQLSelect.SelectedIndex]}.sql", txtSQL.Text, Encoding.UTF8);
        }

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewFile.Text))
            {
                MessageBox.Show(@"文件名不能为空", @"提示");
            }
            else
            {
                string fn = $@"SQL\{Path}\{txtNewFile.Text}.sql";
                if (File.Exists(fn))
                {
                    MessageBox.Show(@"文件已存在", @"提示");
                }
                else
                {
                    File.WriteAllText(fn, txtSQL.Text, Encoding.UTF8);
                    InitTabSql();
                    cbSQLSelect.SelectedIndex = FileName.IndexOf(txtNewFile.Text);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewFile.Text))
            {
                MessageBox.Show(@"文件名不能为空", @"提示");
            }
            else
            {
                string fn = $@"SQL\{Path}\{txtNewFile.Text}.sql";
                if (!File.Exists(fn))
                {
                    MessageBox.Show(@"文件不存在", @"提示");
                }
                else
                {
                    File.Delete(fn);
                    InitTabSql();
                }

            }
        }






        #endregion
        
        #region Tab Config
        private void dbgrdconfig_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cdsConfig.WriteXml($@"{Program.basePath}\config.xml");
        }

        private void dbgrdconfig_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {            
            e.Row.Cells["var_type"].Value = 0;
        }

        private void dbgrdconfig_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            cdsConfig.WriteXml($@"{Program.basePath}\config.xml");
        }

        private void dbgrdconfig_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //cdsConfig.WriteXml($@"{Program.basePath}\config.xml");
        }

        private void dbgrdconfig_CancelRowEdit(object sender, QuestionEventArgs e)
        {
            cdsConfig.WriteXml($@"{Program.basePath}\config.xml");
        }

        private void dbgrdconfig_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        #endregion

        #region Tab Plan

        private bool canChangePlan = true;
        private List<PlanData> dsPlan;
        private List<PlanDataItem> dsPlanData;
        private bool LoadItem = false;
        private void lvPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (lvPlan.SelectedIndex > -1)
            {
                PlanData dr = dsPlan[lvPlan.SelectedIndex];
                txtPlanName.Text = dr.Name;
                cbEnabled.Checked = dr.Enable;
                cbPlanType.SelectedIndex = cbPlanType.Items.IndexOf(dr.PlanDateModel.ToString());
                dtOnceDate.Value = dr.PlanDate;
                dtOnceTime.Value = dr.PlanTime;
                cbRepeatRate.SelectedIndex = cbRepeatRate.Items.IndexOf(dr.PlanTimeModel.ToString());
                nudRepeatStep.Value = dr.PlanDayStep;
                dtRepeatTime.Value = dr.PlanTime;
                nudStepMinutes.Value = dr.PlanTimeStep;
                lblLastSuccessTime.Text = dr.LastSuccessTime.ToString("yyyy-MM-dd HH:mm:ss");

                //DoWeekDays
                cbMonday.Checked = $@"{dr.PlanWeek:0}".IndexOf("1",StringComparison.CurrentCulture) > -1;
                cbTuesday.Checked = $@"{dr.PlanWeek:0}".IndexOf("2",StringComparison.CurrentCulture) > -1;
                cbWednesday.Checked = $@"{dr.PlanWeek:0}".IndexOf("3",StringComparison.CurrentCulture) > -1;
                cbThursday.Checked = $@"{dr.PlanWeek:0}".IndexOf("4",StringComparison.CurrentCulture) > -1;
                cbFriday.Checked = $@"{dr.PlanWeek:0}".IndexOf("5",StringComparison.CurrentCulture) > -1;
                cbSaturday.Checked = $@"{dr.PlanWeek:0}".IndexOf("6",StringComparison.CurrentCulture) > -1;
                cbSunday.Checked = $@"{dr.PlanWeek:0}".IndexOf("7",StringComparison.CurrentCulture) > -1;
                LoadPlanDataItem(dr);

                //Refresh Value States
                pnlStep.Enabled = cbEnabled.Enabled = cbPlanType.Enabled = txtPlanName.Enabled = true;
                lvPlan.Enabled = true;
                canChangePlan = true;

                btnRemovePlan.Enabled = btnTestPlan.Enabled = true;
                btnSavePlan.Enabled = false;
            }
            else
            {
                InitPlanPanel();
            }
            
        }

        dynamic fm = new[] {new {Name="退出执行",Value=0 }, new { Name="忽略错误",Value=1}};
        private void LoadPlanDataItem(PlanData dr)
        {
            LoadItem = true;
            dsPlanData = PlanHelper.Create().GetPlanItemData(dr.ID);
            FailMode.DataSource = fm;
            FailMode.DisplayMember = "Name";
            FailMode.ValueMember = "Value";
            dgvSqlSteps.DataSource = dsPlanData;
            dgvSqlSteps.Tag = dr;
            dgvSqlSteps.Refresh();
            LoadItem = false;

        }

        private void btnSavePlan_Click(object sender, EventArgs e)
        {
            PlanData data = dsPlan[lvPlan.SelectedIndex];
            data.Name = txtPlanName.Text;
            data.Enable = cbEnabled.Checked;
            data.PlanDateModel = (PlanDateModel) Enum.Parse(typeof(PlanDateModel), cbPlanType.SelectedItem.ToString());
            data.PlanDate = dtOnceDate.Value;
            data.PlanTimeModel =
                (PlanTimeModel) Enum.Parse(typeof(PlanTimeModel), cbRepeatRate.SelectedItem.ToString());
            data.PlanDayStep = (int)nudRepeatStep.Value;
            data.PlanTimeStep = (int) nudStepMinutes.Value;

            if (data.PlanDateModel == PlanDateModel.执行一次)
            {
                data.PlanTime = dtOnceTime.Value;
            }
            else
            {
                data.PlanTime = dtRepeatTime.Value;
            }

            string week = "";
            week += cbMonday.Checked ? "1":"";
            week += cbTuesday.Checked ? "2":"";
            week += cbWednesday.Checked ? "3":"";
            week += cbThursday.Checked ? "4":"";
            week += cbFriday.Checked ? "5":"";
            week += cbSaturday.Checked ? "6":"";
            week += cbSunday.Checked ? "7":"";
            data.PlanWeek = week == "" ? 0:int.Parse(week);
            
            PlanHelper.Create().UpdatePlanData(data);
            lvPlan.Enabled = true;
            LoadPlanData();
            btnSavePlan.Enabled = false;
        }

        private void btnNewPlan_Click(object sender, EventArgs e)
        {
            PlanData data=PlanData.Create();
            PlanHelper.Create().AddPlanData(data);
            
            LoadPlanData();
            lvPlan.SelectedIndex = lvPlan.Items.Count-1;
        } 

        private void btnRemovePlan_Click(object sender, EventArgs e)
        {
            PlanData data=dsPlan[lvPlan.SelectedIndex];
            PlanHelper.Create().DeletePlanData(data);
            
            LoadPlanData();
            lvPlan_SelectedIndexChanged(lvPlan,e);
        }

        private void btnTestPlan_Click(object sender, EventArgs e)
        {
            //此处运行计划测试
            frmSync sync=new frmSync();
            PlanData pd = dsPlan[lvPlan.SelectedIndex];
            sync.PlanData = pd;
            sync.ShowDialog();

        }

        private void ValueChanged(object sender, EventArgs e)
        {
            lvPlan.Enabled = false;
            canChangePlan = false;
            btnRemovePlan.Enabled = btnTestPlan.Enabled = false;
            btnSavePlan.Enabled = true;
            EventHandler evt;
            if (sender == cbPlanType)
            {
                evt= (a, b) =>
                {
                    if (cbPlanType.SelectedIndex == 0)
                    {
                        pnlOnce.Enabled = true;
                        pnlRate.Enabled = false;
                    }
                    else
                    {
                        pnlOnce.Enabled = false;
                        pnlRate.Enabled = true;
                    }
                };
                evt(sender, e);
            }
            else if (sender == cbRepeatRate)
            {
                evt = (a, b) =>
                {
                    lblStepUnit.Text =
                            (PlanTimeModel) Enum.Parse(typeof(PlanTimeModel), cbRepeatRate.SelectedItem.ToString()) ==
                            PlanTimeModel.每周
                                ? "周"
                                : "天";
                    cbMonday.Enabled = cbTuesday.Enabled = cbWednesday.Enabled = cbThursday.Enabled = cbFriday.Enabled =
                        cbSaturday.Enabled = cbSunday.Enabled =
                            (PlanTimeModel) Enum.Parse(typeof(PlanTimeModel), cbRepeatRate.SelectedItem.ToString()) ==
                            PlanTimeModel.每周;
                };
                evt(sender, e);
            }
        }
        
        private void dgvSqlSteps_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ValueChanged(sender,e);
            }
            
        }

        private void InitPlanPanel()
        {
            cbPlanType.SelectedIndex = 0;
            cbRepeatRate.SelectedIndex = 0;
        }

        private void LoadPlanData()
        {
            int index = lvPlan.SelectedIndex;
            dsPlan=PlanHelper.Create().GetPlanData();
            lvPlan.DataSource = dsPlan;
            lvPlan.DisplayMember = "Name";
            lvPlan.ValueMember = "ID";
            lvPlan.Refresh();
            if (lvPlan.Items.Count >= index + 1)
            {
                lvPlan.SelectedIndex = index;
            }
            else
            {
                lvPlan.SelectedIndex = lvPlan.Items.Count-1;
            }

            if (index == -1 && lvPlan.Items.Count > 0)
            {
                lvPlan.SelectedIndex = 0;
            }

            if (lvPlan.Items.Count == 0)
            {
                pnlOnce.Enabled = pnlRate.Enabled =
                    pnlStep.Enabled = cbEnabled.Enabled = cbPlanType.Enabled = txtPlanName.Enabled = false;
            }
            

            //Refresh Value States
            lvPlan.Enabled = true;
            canChangePlan = true;
        }
        
        private void btnNewStep_Click(object sender, EventArgs e)
        {
            if (dgvSqlSteps.Tag != null && (!LoadItem))
            {
                PlanData dr=(PlanData)dgvSqlSteps.Tag;
                
                PlanDataItem item=PlanDataItem.Create();
                item.PlanID = dr.ID;
                PlanHelper.Create().AddPlanItemData(item);
                LoadPlanDataItem(dr);
                int index=dgvSqlSteps.Rows.Count-1;
                DataGridViewCellCollection cells=dgvSqlSteps.Rows[index].Cells;
                dgvSqlSteps.CurrentCell=cells[1];
                dgvSqlSteps.BeginEdit(true);
            }
        }

        private void btnSaveStep_Click(object sender, EventArgs e)
        {
            if (dgvSqlSteps.Tag != null && (!LoadItem))
            {
                PlanData dr=(PlanData)dgvSqlSteps.Tag;
                int index=dgvSqlSteps.SelectedRows[0].Index;
                DataGridViewCellCollection cells=dgvSqlSteps.Rows[index].Cells;
                PlanDataItem item=PlanDataItem.Create();
                item.PlanID = dr.ID;
                item.PlanDataID = (int)cells["PlanDataID_col"].Value;
                item.PlanDataName = cells["PlanDataName_col"].Value.ToString();
                item.PlanSql = cells["PlanSql_col"].Value.ToString();
                item.Index = (int) cells["Index"].Value;
                item.FailMode = (FailMode) (int)cells["FailMode"].Value;
                PlanHelper.Create().UpdatePlanItemData(item);
                LoadPlanDataItem(dr);
            }
        }

        private void btnRemoveStep_Click(object sender, EventArgs e)
        {
            if (dgvSqlSteps.Tag != null && (!LoadItem))
            {
                PlanData dr=(PlanData)dgvSqlSteps.Tag;
                int index=dgvSqlSteps.SelectedRows[0].Index;
                DataGridViewCellCollection cells=dgvSqlSteps.Rows[index].Cells;
                int PlanDataID = (int)cells["PlanDataID_col"].Value;
                PlanHelper.Create().DeletePlanItemData(PlanDataID);
                LoadPlanDataItem(dr);
            }
        }
        #endregion

    }
}


