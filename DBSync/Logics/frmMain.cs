using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DBSync.Enumerations;
using DBSync.Ini;
using DBSync.Log;
using DBSync.Services;
using MySql.Data.MySqlClient;

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
            InitTabLog();
            InitTabSrc();
            InitTabDst();
            InitTabSql();
            InitDebugMode();
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
                txtMsg.Text = ex.Message;
            }

            con.Close();
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

        
    }
}


