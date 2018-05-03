namespace DBSync.Logics
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTab = new System.Windows.Forms.TabControl();
            this.tLog = new System.Windows.Forms.TabPage();
            this.dtpLog = new System.Windows.Forms.DateTimePicker();
            this.btnRLog = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.gridLog = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coldate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tSrc = new System.Windows.Forms.TabPage();
            this.btnSrcTest = new System.Windows.Forms.Button();
            this.btnSrcApply = new System.Windows.Forms.Button();
            this.txtSrcDB = new System.Windows.Forms.TextBox();
            this.txtSrcPassword = new System.Windows.Forms.TextBox();
            this.txtSrcUser = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSrcPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSrcSource = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSrcAuthz = new System.Windows.Forms.ComboBox();
            this.cbSrcType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tTarget = new System.Windows.Forms.TabPage();
            this.btnDstTest = new System.Windows.Forms.Button();
            this.btnDstApply = new System.Windows.Forms.Button();
            this.txtDstDB = new System.Windows.Forms.TextBox();
            this.txtDstPassword = new System.Windows.Forms.TextBox();
            this.txtDstUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDstPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDstSource = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbDstAuthz = new System.Windows.Forms.ComboBox();
            this.cbDstType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tSQL = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewFile = new System.Windows.Forms.Button();
            this.txtNewFile = new System.Windows.Forms.TextBox();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.rbSQLDst = new System.Windows.Forms.RadioButton();
            this.rbSQLSrc = new System.Windows.Forms.RadioButton();
            this.cbSQLSelect = new System.Windows.Forms.ComboBox();
            this.tSQLTest = new System.Windows.Forms.TabPage();
            this.tabResult = new System.Windows.Forms.TabControl();
            this.tabGrid = new System.Windows.Forms.TabPage();
            this.scGrid = new System.Windows.Forms.SplitContainer();
            this.lbGrid = new System.Windows.Forms.ListBox();
            this.gridTest = new System.Windows.Forms.DataGridView();
            this.tabMsg = new System.Windows.Forms.TabPage();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.rbDst = new System.Windows.Forms.RadioButton();
            this.rbSrc = new System.Windows.Forms.RadioButton();
            this.cbTestDebug = new System.Windows.Forms.CheckBox();
            this.btnTestExec = new System.Windows.Forms.Button();
            this.txtTestSQL = new System.Windows.Forms.TextBox();
            this.tConfig = new System.Windows.Forms.TabPage();
            this.dbgrdconfig = new System.Windows.Forms.DataGridView();
            this.cdsConfig = new System.Data.DataSet();
            this.global_var = new System.Data.DataTable();
            this.var_name_col = new System.Data.DataColumn();
            this.var_type_col = new System.Data.DataColumn();
            this.var_value_col = new System.Data.DataColumn();
            this.zd_var_type = new System.Data.DataTable();
            this.type_id_col = new System.Data.DataColumn();
            this.type_name_col = new System.Data.DataColumn();
            this.tPlan = new System.Windows.Forms.TabPage();
            this.lblLastSuccessTime = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lvPlan = new System.Windows.Forms.ListBox();
            this.btnTestPlan = new System.Windows.Forms.Button();
            this.pnlStep = new System.Windows.Forms.Panel();
            this.btnSaveStep = new System.Windows.Forms.Button();
            this.btnRemoveStep = new System.Windows.Forms.Button();
            this.btnNewStep = new System.Windows.Forms.Button();
            this.dgvSqlSteps = new System.Windows.Forms.DataGridView();
            this.PlanID_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlanDataID_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlanDataName_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlanSql_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FailMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlRate = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.nudStepMinutes = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.cbSunday = new System.Windows.Forms.CheckBox();
            this.cbThursday = new System.Windows.Forms.CheckBox();
            this.dtRepeatTime = new System.Windows.Forms.DateTimePicker();
            this.cbTuesday = new System.Windows.Forms.CheckBox();
            this.cbSaturday = new System.Windows.Forms.CheckBox();
            this.cbFriday = new System.Windows.Forms.CheckBox();
            this.cbWednesday = new System.Windows.Forms.CheckBox();
            this.cbMonday = new System.Windows.Forms.CheckBox();
            this.nudRepeatStep = new System.Windows.Forms.NumericUpDown();
            this.lblStepUnit = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cbRepeatRate = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.pnlOnce = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dtOnceTime = new System.Windows.Forms.DateTimePicker();
            this.dtOnceDate = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbPlanType = new System.Windows.Forms.ComboBox();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.txtPlanName = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSavePlan = new System.Windows.Forms.Button();
            this.btnRemovePlan = new System.Windows.Forms.Button();
            this.btnNewPlan = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.svc = new System.ServiceProcess.ServiceController();
            this.var_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.var_type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.var_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainTab.SuspendLayout();
            this.tLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).BeginInit();
            this.tSrc.SuspendLayout();
            this.tTarget.SuspendLayout();
            this.tSQL.SuspendLayout();
            this.tSQLTest.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scGrid)).BeginInit();
            this.scGrid.Panel1.SuspendLayout();
            this.scGrid.Panel2.SuspendLayout();
            this.scGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTest)).BeginInit();
            this.tabMsg.SuspendLayout();
            this.tConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbgrdconfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdsConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.global_var)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zd_var_type)).BeginInit();
            this.tPlan.SuspendLayout();
            this.pnlStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSqlSteps)).BeginInit();
            this.pnlRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStepMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatStep)).BeginInit();
            this.pnlOnce.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.tLog);
            this.mainTab.Controls.Add(this.tSrc);
            this.mainTab.Controls.Add(this.tTarget);
            this.mainTab.Controls.Add(this.tSQL);
            this.mainTab.Controls.Add(this.tSQLTest);
            this.mainTab.Controls.Add(this.tConfig);
            this.mainTab.Controls.Add(this.tPlan);
            this.mainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTab.Location = new System.Drawing.Point(0, 0);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(896, 718);
            this.mainTab.TabIndex = 0;
            // 
            // tLog
            // 
            this.tLog.Controls.Add(this.dtpLog);
            this.tLog.Controls.Add(this.btnRLog);
            this.tLog.Controls.Add(this.txtLog);
            this.tLog.Controls.Add(this.gridLog);
            this.tLog.Location = new System.Drawing.Point(4, 22);
            this.tLog.Name = "tLog";
            this.tLog.Padding = new System.Windows.Forms.Padding(3);
            this.tLog.Size = new System.Drawing.Size(888, 692);
            this.tLog.TabIndex = 0;
            this.tLog.Text = "日志";
            this.tLog.UseVisualStyleBackColor = true;
            // 
            // dtpLog
            // 
            this.dtpLog.CustomFormat = "yyyy-MM-dd";
            this.dtpLog.Location = new System.Drawing.Point(124, 18);
            this.dtpLog.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.dtpLog.MinDate = new System.DateTime(2001, 1, 1, 0, 0, 0, 0);
            this.dtpLog.Name = "dtpLog";
            this.dtpLog.Size = new System.Drawing.Size(200, 21);
            this.dtpLog.TabIndex = 4;
            // 
            // btnRLog
            // 
            this.btnRLog.Location = new System.Drawing.Point(19, 17);
            this.btnRLog.Name = "btnRLog";
            this.btnRLog.Size = new System.Drawing.Size(98, 23);
            this.btnRLog.TabIndex = 3;
            this.btnRLog.Text = "刷新日志列表";
            this.btnRLog.UseVisualStyleBackColor = true;
            this.btnRLog.Click += new System.EventHandler(this.btnRLog_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(19, 369);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(838, 307);
            this.txtLog.TabIndex = 2;
            // 
            // gridLog
            // 
            this.gridLog.AllowUserToAddRows = false;
            this.gridLog.AllowUserToDeleteRows = false;
            this.gridLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.coldate,
            this.colSrc});
            this.gridLog.Location = new System.Drawing.Point(19, 46);
            this.gridLog.MultiSelect = false;
            this.gridLog.Name = "gridLog";
            this.gridLog.ReadOnly = true;
            this.gridLog.RowTemplate.Height = 23;
            this.gridLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLog.Size = new System.Drawing.Size(838, 308);
            this.gridLog.TabIndex = 1;
            this.gridLog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLog_CellClick);
            this.gridLog.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLog_CellClick);
            // 
            // colType
            // 
            this.colType.DataPropertyName = "LogLevel";
            this.colType.Frozen = true;
            this.colType.HeaderText = "日志类型";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // coldate
            // 
            this.coldate.DataPropertyName = "LogTime";
            this.coldate.Frozen = true;
            this.coldate.HeaderText = "发生时间";
            this.coldate.Name = "coldate";
            this.coldate.ReadOnly = true;
            this.coldate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.coldate.Width = 200;
            // 
            // colSrc
            // 
            this.colSrc.DataPropertyName = "Source";
            this.colSrc.Frozen = true;
            this.colSrc.HeaderText = "发生来源";
            this.colSrc.Name = "colSrc";
            this.colSrc.ReadOnly = true;
            this.colSrc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSrc.Width = 300;
            // 
            // tSrc
            // 
            this.tSrc.Controls.Add(this.btnSrcTest);
            this.tSrc.Controls.Add(this.btnSrcApply);
            this.tSrc.Controls.Add(this.txtSrcDB);
            this.tSrc.Controls.Add(this.txtSrcPassword);
            this.tSrc.Controls.Add(this.txtSrcUser);
            this.tSrc.Controls.Add(this.label7);
            this.tSrc.Controls.Add(this.txtSrcPort);
            this.tSrc.Controls.Add(this.label6);
            this.tSrc.Controls.Add(this.txtSrcSource);
            this.tSrc.Controls.Add(this.label3);
            this.tSrc.Controls.Add(this.cbSrcAuthz);
            this.tSrc.Controls.Add(this.cbSrcType);
            this.tSrc.Controls.Add(this.label5);
            this.tSrc.Controls.Add(this.label4);
            this.tSrc.Controls.Add(this.label2);
            this.tSrc.Controls.Add(this.label1);
            this.tSrc.Location = new System.Drawing.Point(4, 22);
            this.tSrc.Name = "tSrc";
            this.tSrc.Padding = new System.Windows.Forms.Padding(3);
            this.tSrc.Size = new System.Drawing.Size(888, 692);
            this.tSrc.TabIndex = 1;
            this.tSrc.Text = "源数据库设置";
            this.tSrc.UseVisualStyleBackColor = true;
            // 
            // btnSrcTest
            // 
            this.btnSrcTest.Location = new System.Drawing.Point(198, 299);
            this.btnSrcTest.Name = "btnSrcTest";
            this.btnSrcTest.Size = new System.Drawing.Size(75, 23);
            this.btnSrcTest.TabIndex = 20;
            this.btnSrcTest.Text = "测试";
            this.btnSrcTest.UseVisualStyleBackColor = true;
            this.btnSrcTest.Click += new System.EventHandler(this.btnSrcTest_Click);
            // 
            // btnSrcApply
            // 
            this.btnSrcApply.Location = new System.Drawing.Point(93, 299);
            this.btnSrcApply.Name = "btnSrcApply";
            this.btnSrcApply.Size = new System.Drawing.Size(75, 23);
            this.btnSrcApply.TabIndex = 3;
            this.btnSrcApply.Text = "应用";
            this.btnSrcApply.UseVisualStyleBackColor = true;
            this.btnSrcApply.Click += new System.EventHandler(this.btnSrcApply_Click);
            // 
            // txtSrcDB
            // 
            this.txtSrcDB.Location = new System.Drawing.Point(93, 256);
            this.txtSrcDB.Name = "txtSrcDB";
            this.txtSrcDB.Size = new System.Drawing.Size(221, 21);
            this.txtSrcDB.TabIndex = 2;
            // 
            // txtSrcPassword
            // 
            this.txtSrcPassword.Location = new System.Drawing.Point(93, 217);
            this.txtSrcPassword.Name = "txtSrcPassword";
            this.txtSrcPassword.Size = new System.Drawing.Size(221, 21);
            this.txtSrcPassword.TabIndex = 2;
            this.txtSrcPassword.UseSystemPasswordChar = true;
            // 
            // txtSrcUser
            // 
            this.txtSrcUser.Location = new System.Drawing.Point(93, 179);
            this.txtSrcUser.Name = "txtSrcUser";
            this.txtSrcUser.Size = new System.Drawing.Size(221, 21);
            this.txtSrcUser.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "数据库";
            // 
            // txtSrcPort
            // 
            this.txtSrcPort.Location = new System.Drawing.Point(93, 104);
            this.txtSrcPort.Name = "txtSrcPort";
            this.txtSrcPort.Size = new System.Drawing.Size(221, 21);
            this.txtSrcPort.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "密码";
            // 
            // txtSrcSource
            // 
            this.txtSrcSource.Location = new System.Drawing.Point(93, 66);
            this.txtSrcSource.Name = "txtSrcSource";
            this.txtSrcSource.Size = new System.Drawing.Size(221, 21);
            this.txtSrcSource.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "实例端口";
            // 
            // cbSrcAuthz
            // 
            this.cbSrcAuthz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcAuthz.FormattingEnabled = true;
            this.cbSrcAuthz.Items.AddRange(new object[] {
            "Windows 身份认证",
            "密码认证"});
            this.cbSrcAuthz.Location = new System.Drawing.Point(93, 142);
            this.cbSrcAuthz.Name = "cbSrcAuthz";
            this.cbSrcAuthz.Size = new System.Drawing.Size(221, 20);
            this.cbSrcAuthz.TabIndex = 1;
            this.cbSrcAuthz.SelectedIndexChanged += new System.EventHandler(this.cbSrcAuthz_SelectedIndexChanged);
            // 
            // cbSrcType
            // 
            this.cbSrcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcType.FormattingEnabled = true;
            this.cbSrcType.Items.AddRange(new object[] {
            "SQL Server",
            "MySQL"});
            this.cbSrcType.Location = new System.Drawing.Point(93, 33);
            this.cbSrcType.Name = "cbSrcType";
            this.cbSrcType.Size = new System.Drawing.Size(221, 20);
            this.cbSrcType.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "用户名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "认证方式";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "实例名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库类型";
            // 
            // tTarget
            // 
            this.tTarget.Controls.Add(this.btnDstTest);
            this.tTarget.Controls.Add(this.btnDstApply);
            this.tTarget.Controls.Add(this.txtDstDB);
            this.tTarget.Controls.Add(this.txtDstPassword);
            this.tTarget.Controls.Add(this.txtDstUser);
            this.tTarget.Controls.Add(this.label8);
            this.tTarget.Controls.Add(this.txtDstPort);
            this.tTarget.Controls.Add(this.label9);
            this.tTarget.Controls.Add(this.txtDstSource);
            this.tTarget.Controls.Add(this.label10);
            this.tTarget.Controls.Add(this.cbDstAuthz);
            this.tTarget.Controls.Add(this.cbDstType);
            this.tTarget.Controls.Add(this.label11);
            this.tTarget.Controls.Add(this.label12);
            this.tTarget.Controls.Add(this.label13);
            this.tTarget.Controls.Add(this.label14);
            this.tTarget.Location = new System.Drawing.Point(4, 22);
            this.tTarget.Name = "tTarget";
            this.tTarget.Padding = new System.Windows.Forms.Padding(3);
            this.tTarget.Size = new System.Drawing.Size(888, 692);
            this.tTarget.TabIndex = 2;
            this.tTarget.Text = "目标数据库设置";
            this.tTarget.UseVisualStyleBackColor = true;
            // 
            // btnDstTest
            // 
            this.btnDstTest.Location = new System.Drawing.Point(198, 299);
            this.btnDstTest.Name = "btnDstTest";
            this.btnDstTest.Size = new System.Drawing.Size(75, 23);
            this.btnDstTest.TabIndex = 19;
            this.btnDstTest.Text = "测试";
            this.btnDstTest.UseVisualStyleBackColor = true;
            this.btnDstTest.Click += new System.EventHandler(this.btnDstTest_Click);
            // 
            // btnDstApply
            // 
            this.btnDstApply.Location = new System.Drawing.Point(93, 299);
            this.btnDstApply.Name = "btnDstApply";
            this.btnDstApply.Size = new System.Drawing.Size(75, 23);
            this.btnDstApply.TabIndex = 18;
            this.btnDstApply.Text = "应用";
            this.btnDstApply.UseVisualStyleBackColor = true;
            this.btnDstApply.Click += new System.EventHandler(this.btnDstApply_Click);
            // 
            // txtDstDB
            // 
            this.txtDstDB.Location = new System.Drawing.Point(93, 256);
            this.txtDstDB.Name = "txtDstDB";
            this.txtDstDB.Size = new System.Drawing.Size(221, 21);
            this.txtDstDB.TabIndex = 13;
            // 
            // txtDstPassword
            // 
            this.txtDstPassword.Location = new System.Drawing.Point(93, 217);
            this.txtDstPassword.Name = "txtDstPassword";
            this.txtDstPassword.Size = new System.Drawing.Size(221, 21);
            this.txtDstPassword.TabIndex = 14;
            this.txtDstPassword.UseSystemPasswordChar = true;
            // 
            // txtDstUser
            // 
            this.txtDstUser.Location = new System.Drawing.Point(93, 179);
            this.txtDstUser.Name = "txtDstUser";
            this.txtDstUser.Size = new System.Drawing.Size(221, 21);
            this.txtDstUser.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 259);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "数据库";
            // 
            // txtDstPort
            // 
            this.txtDstPort.Location = new System.Drawing.Point(93, 104);
            this.txtDstPort.Name = "txtDstPort";
            this.txtDstPort.Size = new System.Drawing.Size(221, 21);
            this.txtDstPort.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 220);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "密码";
            // 
            // txtDstSource
            // 
            this.txtDstSource.Location = new System.Drawing.Point(93, 66);
            this.txtDstSource.Name = "txtDstSource";
            this.txtDstSource.Size = new System.Drawing.Size(221, 21);
            this.txtDstSource.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 107);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "实例端口";
            // 
            // cbDstAuthz
            // 
            this.cbDstAuthz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstAuthz.FormattingEnabled = true;
            this.cbDstAuthz.Items.AddRange(new object[] {
            "Windows 身份认证",
            "密码认证"});
            this.cbDstAuthz.Location = new System.Drawing.Point(93, 142);
            this.cbDstAuthz.Name = "cbDstAuthz";
            this.cbDstAuthz.Size = new System.Drawing.Size(221, 20);
            this.cbDstAuthz.TabIndex = 11;
            this.cbDstAuthz.SelectedIndexChanged += new System.EventHandler(this.cbDstAuthz_SelectedIndexChanged);
            // 
            // cbDstType
            // 
            this.cbDstType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstType.FormattingEnabled = true;
            this.cbDstType.Items.AddRange(new object[] {
            "SQL Server",
            "MySQL"});
            this.cbDstType.Location = new System.Drawing.Point(93, 33);
            this.cbDstType.Name = "cbDstType";
            this.cbDstType.Size = new System.Drawing.Size(221, 20);
            this.cbDstType.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 182);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 7;
            this.label11.Text = "用户名";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(22, 145);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "认证方式";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(22, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 9;
            this.label13.Text = "实例名称";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(22, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 10;
            this.label14.Text = "数据库类型";
            // 
            // tSQL
            // 
            this.tSQL.Controls.Add(this.btnDelete);
            this.tSQL.Controls.Add(this.btnNewFile);
            this.tSQL.Controls.Add(this.txtNewFile);
            this.tSQL.Controls.Add(this.txtSQL);
            this.tSQL.Controls.Add(this.btnSave);
            this.tSQL.Controls.Add(this.btnRefresh);
            this.tSQL.Controls.Add(this.rbSQLDst);
            this.tSQL.Controls.Add(this.rbSQLSrc);
            this.tSQL.Controls.Add(this.cbSQLSelect);
            this.tSQL.Location = new System.Drawing.Point(4, 22);
            this.tSQL.Name = "tSQL";
            this.tSQL.Size = new System.Drawing.Size(888, 692);
            this.tSQL.TabIndex = 3;
            this.tSQL.Text = "SQL设置";
            this.tSQL.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(585, 56);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNewFile
            // 
            this.btnNewFile.Location = new System.Drawing.Point(504, 56);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(75, 23);
            this.btnNewFile.TabIndex = 5;
            this.btnNewFile.Text = "新建";
            this.btnNewFile.UseVisualStyleBackColor = true;
            this.btnNewFile.Click += new System.EventHandler(this.btnNewFile_Click);
            // 
            // txtNewFile
            // 
            this.txtNewFile.Location = new System.Drawing.Point(348, 58);
            this.txtNewFile.Name = "txtNewFile";
            this.txtNewFile.Size = new System.Drawing.Size(150, 21);
            this.txtNewFile.TabIndex = 4;
            // 
            // txtSQL
            // 
            this.txtSQL.Location = new System.Drawing.Point(42, 91);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSQL.Size = new System.Drawing.Size(618, 565);
            this.txtSQL.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(585, 24);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(504, 24);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // rbSQLDst
            // 
            this.rbSQLDst.AutoSize = true;
            this.rbSQLDst.Location = new System.Drawing.Point(383, 27);
            this.rbSQLDst.Name = "rbSQLDst";
            this.rbSQLDst.Size = new System.Drawing.Size(65, 16);
            this.rbSQLDst.TabIndex = 1;
            this.rbSQLDst.Text = "目标SQL";
            this.rbSQLDst.UseVisualStyleBackColor = true;
            // 
            // rbSQLSrc
            // 
            this.rbSQLSrc.AutoSize = true;
            this.rbSQLSrc.Location = new System.Drawing.Point(324, 27);
            this.rbSQLSrc.Name = "rbSQLSrc";
            this.rbSQLSrc.Size = new System.Drawing.Size(53, 16);
            this.rbSQLSrc.TabIndex = 1;
            this.rbSQLSrc.Text = "源SQL";
            this.rbSQLSrc.UseVisualStyleBackColor = true;
            this.rbSQLSrc.CheckedChanged += new System.EventHandler(this.rbSQLSrc_CheckedChanged);
            // 
            // cbSQLSelect
            // 
            this.cbSQLSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSQLSelect.FormattingEnabled = true;
            this.cbSQLSelect.Location = new System.Drawing.Point(42, 26);
            this.cbSQLSelect.Name = "cbSQLSelect";
            this.cbSQLSelect.Size = new System.Drawing.Size(264, 20);
            this.cbSQLSelect.TabIndex = 0;
            this.cbSQLSelect.SelectedIndexChanged += new System.EventHandler(this.cbSQLSelect_SelectedIndexChanged);
            // 
            // tSQLTest
            // 
            this.tSQLTest.Controls.Add(this.tabResult);
            this.tSQLTest.Controls.Add(this.rbDst);
            this.tSQLTest.Controls.Add(this.rbSrc);
            this.tSQLTest.Controls.Add(this.cbTestDebug);
            this.tSQLTest.Controls.Add(this.btnTestExec);
            this.tSQLTest.Controls.Add(this.txtTestSQL);
            this.tSQLTest.Location = new System.Drawing.Point(4, 22);
            this.tSQLTest.Name = "tSQLTest";
            this.tSQLTest.Size = new System.Drawing.Size(888, 692);
            this.tSQLTest.TabIndex = 4;
            this.tSQLTest.Text = "SQL调试";
            this.tSQLTest.UseVisualStyleBackColor = true;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.tabGrid);
            this.tabResult.Controls.Add(this.tabMsg);
            this.tabResult.Location = new System.Drawing.Point(20, 421);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.Size = new System.Drawing.Size(668, 249);
            this.tabResult.TabIndex = 5;
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.scGrid);
            this.tabGrid.Location = new System.Drawing.Point(4, 22);
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.Size = new System.Drawing.Size(660, 223);
            this.tabGrid.TabIndex = 0;
            this.tabGrid.Text = "结果";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // scGrid
            // 
            this.scGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scGrid.IsSplitterFixed = true;
            this.scGrid.Location = new System.Drawing.Point(0, 0);
            this.scGrid.Name = "scGrid";
            // 
            // scGrid.Panel1
            // 
            this.scGrid.Panel1.Controls.Add(this.lbGrid);
            // 
            // scGrid.Panel2
            // 
            this.scGrid.Panel2.Controls.Add(this.gridTest);
            this.scGrid.Size = new System.Drawing.Size(660, 223);
            this.scGrid.SplitterDistance = 116;
            this.scGrid.TabIndex = 1;
            // 
            // lbGrid
            // 
            this.lbGrid.DisplayMember = "TableName";
            this.lbGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbGrid.FormattingEnabled = true;
            this.lbGrid.ItemHeight = 12;
            this.lbGrid.Location = new System.Drawing.Point(0, 0);
            this.lbGrid.Name = "lbGrid";
            this.lbGrid.Size = new System.Drawing.Size(116, 223);
            this.lbGrid.TabIndex = 0;
            this.lbGrid.SelectedIndexChanged += new System.EventHandler(this.lbGrid_SelectedIndexChanged);
            // 
            // gridTest
            // 
            this.gridTest.AllowUserToAddRows = false;
            this.gridTest.AllowUserToDeleteRows = false;
            this.gridTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTest.Location = new System.Drawing.Point(0, 0);
            this.gridTest.Name = "gridTest";
            this.gridTest.ReadOnly = true;
            this.gridTest.RowTemplate.Height = 23;
            this.gridTest.Size = new System.Drawing.Size(540, 223);
            this.gridTest.TabIndex = 0;
            // 
            // tabMsg
            // 
            this.tabMsg.Controls.Add(this.txtMsg);
            this.tabMsg.Location = new System.Drawing.Point(4, 22);
            this.tabMsg.Name = "tabMsg";
            this.tabMsg.Size = new System.Drawing.Size(660, 223);
            this.tabMsg.TabIndex = 1;
            this.tabMsg.Text = "消息";
            this.tabMsg.UseVisualStyleBackColor = true;
            // 
            // txtMsg
            // 
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.Location = new System.Drawing.Point(0, 0);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsg.Size = new System.Drawing.Size(660, 223);
            this.txtMsg.TabIndex = 4;
            // 
            // rbDst
            // 
            this.rbDst.AutoSize = true;
            this.rbDst.Location = new System.Drawing.Point(135, 361);
            this.rbDst.Name = "rbDst";
            this.rbDst.Size = new System.Drawing.Size(107, 16);
            this.rbDst.TabIndex = 3;
            this.rbDst.Text = "使用目标数据库";
            this.rbDst.UseVisualStyleBackColor = true;
            // 
            // rbSrc
            // 
            this.rbSrc.AutoSize = true;
            this.rbSrc.Checked = true;
            this.rbSrc.Location = new System.Drawing.Point(34, 361);
            this.rbSrc.Name = "rbSrc";
            this.rbSrc.Size = new System.Drawing.Size(95, 16);
            this.rbSrc.TabIndex = 3;
            this.rbSrc.TabStop = true;
            this.rbSrc.Text = "使用源数据库";
            this.rbSrc.UseVisualStyleBackColor = true;
            // 
            // cbTestDebug
            // 
            this.cbTestDebug.AutoSize = true;
            this.cbTestDebug.Checked = true;
            this.cbTestDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTestDebug.Location = new System.Drawing.Point(480, 362);
            this.cbTestDebug.Name = "cbTestDebug";
            this.cbTestDebug.Size = new System.Drawing.Size(72, 16);
            this.cbTestDebug.TabIndex = 2;
            this.cbTestDebug.Text = "调试模式";
            this.cbTestDebug.UseVisualStyleBackColor = true;
            // 
            // btnTestExec
            // 
            this.btnTestExec.Location = new System.Drawing.Point(568, 358);
            this.btnTestExec.Name = "btnTestExec";
            this.btnTestExec.Size = new System.Drawing.Size(75, 23);
            this.btnTestExec.TabIndex = 1;
            this.btnTestExec.Text = "执行";
            this.btnTestExec.UseVisualStyleBackColor = true;
            this.btnTestExec.Click += new System.EventHandler(this.btnTestExec_Click);
            // 
            // txtTestSQL
            // 
            this.txtTestSQL.Location = new System.Drawing.Point(20, 21);
            this.txtTestSQL.Multiline = true;
            this.txtTestSQL.Name = "txtTestSQL";
            this.txtTestSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestSQL.Size = new System.Drawing.Size(668, 330);
            this.txtTestSQL.TabIndex = 0;
            // 
            // tConfig
            // 
            this.tConfig.Controls.Add(this.dbgrdconfig);
            this.tConfig.Location = new System.Drawing.Point(4, 22);
            this.tConfig.Name = "tConfig";
            this.tConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tConfig.Size = new System.Drawing.Size(888, 692);
            this.tConfig.TabIndex = 5;
            this.tConfig.Text = "变量设置";
            this.tConfig.UseVisualStyleBackColor = true;
            // 
            // dbgrdconfig
            // 
            this.dbgrdconfig.AllowUserToResizeColumns = false;
            this.dbgrdconfig.AllowUserToResizeRows = false;
            this.dbgrdconfig.AutoGenerateColumns = false;
            this.dbgrdconfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgrdconfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.var_name,
            this.var_type,
            this.var_value});
            this.dbgrdconfig.DataMember = "global_var";
            this.dbgrdconfig.DataSource = this.cdsConfig;
            this.dbgrdconfig.Location = new System.Drawing.Point(18, 26);
            this.dbgrdconfig.MultiSelect = false;
            this.dbgrdconfig.Name = "dbgrdconfig";
            this.dbgrdconfig.RowTemplate.Height = 23;
            this.dbgrdconfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dbgrdconfig.Size = new System.Drawing.Size(841, 632);
            this.dbgrdconfig.TabIndex = 0;
            this.dbgrdconfig.CancelRowEdit += new System.Windows.Forms.QuestionEventHandler(this.dbgrdconfig_CancelRowEdit);
            this.dbgrdconfig.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbgrdconfig_CellEndEdit);
            this.dbgrdconfig.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dbgrdconfig_DataError);
            this.dbgrdconfig.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dbgrdconfig_DefaultValuesNeeded);
            this.dbgrdconfig.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dbgrdconfig_RowsAdded);
            this.dbgrdconfig.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dbgrdconfig_RowsRemoved);
            // 
            // cdsConfig
            // 
            this.cdsConfig.DataSetName = "cdsConfig";
            this.cdsConfig.Namespace = "https://kurosawa.ruby.ne.jp/dbconf/";
            this.cdsConfig.Prefix = "config";
            this.cdsConfig.Tables.AddRange(new System.Data.DataTable[] {
            this.global_var,
            this.zd_var_type});
            // 
            // global_var
            // 
            this.global_var.Columns.AddRange(new System.Data.DataColumn[] {
            this.var_name_col,
            this.var_type_col,
            this.var_value_col});
            this.global_var.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "var_name"}, true)});
            this.global_var.Prefix = "config";
            this.global_var.PrimaryKey = new System.Data.DataColumn[] {
        this.var_name_col};
            this.global_var.TableName = "global_var";
            // 
            // var_name_col
            // 
            this.var_name_col.AllowDBNull = false;
            this.var_name_col.ColumnName = "var_name";
            this.var_name_col.Prefix = "config";
            // 
            // var_type_col
            // 
            this.var_type_col.AllowDBNull = false;
            this.var_type_col.ColumnName = "var_type";
            this.var_type_col.DataType = typeof(int);
            this.var_type_col.DefaultValue = 0;
            this.var_type_col.Prefix = "config";
            // 
            // var_value_col
            // 
            this.var_value_col.AllowDBNull = false;
            this.var_value_col.ColumnName = "var_value";
            this.var_value_col.Prefix = "config";
            // 
            // zd_var_type
            // 
            this.zd_var_type.Columns.AddRange(new System.Data.DataColumn[] {
            this.type_id_col,
            this.type_name_col});
            this.zd_var_type.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "type_id"}, true)});
            this.zd_var_type.Prefix = "config";
            this.zd_var_type.PrimaryKey = new System.Data.DataColumn[] {
        this.type_id_col};
            this.zd_var_type.TableName = "zd_var_type";
            // 
            // type_id_col
            // 
            this.type_id_col.AllowDBNull = false;
            this.type_id_col.AutoIncrement = true;
            this.type_id_col.ColumnName = "type_id";
            this.type_id_col.DataType = typeof(int);
            this.type_id_col.Prefix = "config";
            // 
            // type_name_col
            // 
            this.type_name_col.AllowDBNull = false;
            this.type_name_col.ColumnName = "type_name";
            this.type_name_col.Prefix = "config";
            // 
            // tPlan
            // 
            this.tPlan.Controls.Add(this.lblLastSuccessTime);
            this.tPlan.Controls.Add(this.label27);
            this.tPlan.Controls.Add(this.lvPlan);
            this.tPlan.Controls.Add(this.btnTestPlan);
            this.tPlan.Controls.Add(this.pnlStep);
            this.tPlan.Controls.Add(this.pnlRate);
            this.tPlan.Controls.Add(this.pnlOnce);
            this.tPlan.Controls.Add(this.label17);
            this.tPlan.Controls.Add(this.cbPlanType);
            this.tPlan.Controls.Add(this.cbEnabled);
            this.tPlan.Controls.Add(this.txtPlanName);
            this.tPlan.Controls.Add(this.label16);
            this.tPlan.Controls.Add(this.btnSavePlan);
            this.tPlan.Controls.Add(this.btnRemovePlan);
            this.tPlan.Controls.Add(this.btnNewPlan);
            this.tPlan.Controls.Add(this.label15);
            this.tPlan.Location = new System.Drawing.Point(4, 22);
            this.tPlan.Name = "tPlan";
            this.tPlan.Size = new System.Drawing.Size(888, 692);
            this.tPlan.TabIndex = 6;
            this.tPlan.Text = "计划设置";
            this.tPlan.UseVisualStyleBackColor = true;
            // 
            // lblLastSuccessTime
            // 
            this.lblLastSuccessTime.AutoSize = true;
            this.lblLastSuccessTime.Location = new System.Drawing.Point(247, 66);
            this.lblLastSuccessTime.Name = "lblLastSuccessTime";
            this.lblLastSuccessTime.Size = new System.Drawing.Size(119, 12);
            this.lblLastSuccessTime.TabIndex = 15;
            this.lblLastSuccessTime.Text = "0000-00-00 00:00:00";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(188, 66);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 14;
            this.label27.Text = "上次完成";
            // 
            // lvPlan
            // 
            this.lvPlan.FormattingEnabled = true;
            this.lvPlan.ItemHeight = 12;
            this.lvPlan.Location = new System.Drawing.Point(11, 35);
            this.lvPlan.Name = "lvPlan";
            this.lvPlan.Size = new System.Drawing.Size(154, 520);
            this.lvPlan.TabIndex = 13;
            this.lvPlan.SelectedIndexChanged += new System.EventHandler(this.lvPlan_SelectedIndexChanged);
            // 
            // btnTestPlan
            // 
            this.btnTestPlan.Location = new System.Drawing.Point(11, 603);
            this.btnTestPlan.Name = "btnTestPlan";
            this.btnTestPlan.Size = new System.Drawing.Size(75, 23);
            this.btnTestPlan.TabIndex = 12;
            this.btnTestPlan.Text = "手动运行";
            this.btnTestPlan.UseVisualStyleBackColor = true;
            this.btnTestPlan.Click += new System.EventHandler(this.btnTestPlan_Click);
            // 
            // pnlStep
            // 
            this.pnlStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStep.Controls.Add(this.btnSaveStep);
            this.pnlStep.Controls.Add(this.btnRemoveStep);
            this.pnlStep.Controls.Add(this.btnNewStep);
            this.pnlStep.Controls.Add(this.dgvSqlSteps);
            this.pnlStep.Controls.Add(this.label25);
            this.pnlStep.Location = new System.Drawing.Point(188, 288);
            this.pnlStep.Name = "pnlStep";
            this.pnlStep.Size = new System.Drawing.Size(684, 348);
            this.pnlStep.TabIndex = 11;
            // 
            // btnSaveStep
            // 
            this.btnSaveStep.Location = new System.Drawing.Point(147, 8);
            this.btnSaveStep.Name = "btnSaveStep";
            this.btnSaveStep.Size = new System.Drawing.Size(75, 23);
            this.btnSaveStep.TabIndex = 2;
            this.btnSaveStep.Text = "修改步骤";
            this.btnSaveStep.UseVisualStyleBackColor = true;
            this.btnSaveStep.Click += new System.EventHandler(this.btnSaveStep_Click);
            // 
            // btnRemoveStep
            // 
            this.btnRemoveStep.Location = new System.Drawing.Point(228, 8);
            this.btnRemoveStep.Name = "btnRemoveStep";
            this.btnRemoveStep.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveStep.TabIndex = 2;
            this.btnRemoveStep.Text = "删除步骤";
            this.btnRemoveStep.UseVisualStyleBackColor = true;
            this.btnRemoveStep.Click += new System.EventHandler(this.btnRemoveStep_Click);
            // 
            // btnNewStep
            // 
            this.btnNewStep.Location = new System.Drawing.Point(66, 8);
            this.btnNewStep.Name = "btnNewStep";
            this.btnNewStep.Size = new System.Drawing.Size(75, 23);
            this.btnNewStep.TabIndex = 2;
            this.btnNewStep.Text = "新建步骤";
            this.btnNewStep.UseVisualStyleBackColor = true;
            this.btnNewStep.Click += new System.EventHandler(this.btnNewStep_Click);
            // 
            // dgvSqlSteps
            // 
            this.dgvSqlSteps.AllowUserToAddRows = false;
            this.dgvSqlSteps.AllowUserToDeleteRows = false;
            this.dgvSqlSteps.AllowUserToResizeColumns = false;
            this.dgvSqlSteps.AllowUserToResizeRows = false;
            this.dgvSqlSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSqlSteps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PlanID_col,
            this.PlanDataID_col,
            this.PlanDataName_col,
            this.PlanSql_col,
            this.FailMode,
            this.Index});
            this.dgvSqlSteps.Location = new System.Drawing.Point(8, 45);
            this.dgvSqlSteps.Name = "dgvSqlSteps";
            this.dgvSqlSteps.RowTemplate.Height = 23;
            this.dgvSqlSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSqlSteps.Size = new System.Drawing.Size(659, 288);
            this.dgvSqlSteps.TabIndex = 1;
            this.dgvSqlSteps.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSqlSteps_CellValueChanged);
            // 
            // PlanID_col
            // 
            this.PlanID_col.HeaderText = "计划号";
            this.PlanID_col.Name = "PlanID_col";
            this.PlanID_col.ReadOnly = true;
            this.PlanID_col.Visible = false;
            // 
            // PlanDataID_col
            // 
            this.PlanDataID_col.DataPropertyName = "PlanDataID";
            this.PlanDataID_col.HeaderText = "步骤序号";
            this.PlanDataID_col.Name = "PlanDataID_col";
            this.PlanDataID_col.ReadOnly = true;
            this.PlanDataID_col.Width = 80;
            // 
            // PlanDataName_col
            // 
            this.PlanDataName_col.DataPropertyName = "PlanDataName";
            this.PlanDataName_col.HeaderText = "步骤名称";
            this.PlanDataName_col.Name = "PlanDataName_col";
            this.PlanDataName_col.Width = 200;
            // 
            // PlanSql_col
            // 
            this.PlanSql_col.DataPropertyName = "PlanSql";
            this.PlanSql_col.HeaderText = "步骤SQL文件";
            this.PlanSql_col.Name = "PlanSql_col";
            this.PlanSql_col.Width = 150;
            // 
            // FailMode
            // 
            this.FailMode.DataPropertyName = "FailModeInt";
            this.FailMode.HeaderText = "失败时的操作";
            this.FailMode.Items.AddRange(new object[] {
            "退出执行",
            "忽略错误"});
            this.FailMode.Name = "FailMode";
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.HeaderText = "执行顺序";
            this.Index.Name = "Index";
            this.Index.Width = 80;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 13);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(29, 12);
            this.label25.TabIndex = 0;
            this.label25.Text = "步骤";
            // 
            // pnlRate
            // 
            this.pnlRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRate.Controls.Add(this.label26);
            this.pnlRate.Controls.Add(this.nudStepMinutes);
            this.pnlRate.Controls.Add(this.label24);
            this.pnlRate.Controls.Add(this.cbSunday);
            this.pnlRate.Controls.Add(this.cbThursday);
            this.pnlRate.Controls.Add(this.dtRepeatTime);
            this.pnlRate.Controls.Add(this.cbTuesday);
            this.pnlRate.Controls.Add(this.cbSaturday);
            this.pnlRate.Controls.Add(this.cbFriday);
            this.pnlRate.Controls.Add(this.cbWednesday);
            this.pnlRate.Controls.Add(this.cbMonday);
            this.pnlRate.Controls.Add(this.nudRepeatStep);
            this.pnlRate.Controls.Add(this.lblStepUnit);
            this.pnlRate.Controls.Add(this.label23);
            this.pnlRate.Controls.Add(this.cbRepeatRate);
            this.pnlRate.Controls.Add(this.label22);
            this.pnlRate.Controls.Add(this.label21);
            this.pnlRate.Location = new System.Drawing.Point(188, 179);
            this.pnlRate.Name = "pnlRate";
            this.pnlRate.Size = new System.Drawing.Size(684, 103);
            this.pnlRate.TabIndex = 10;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(586, 29);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(29, 12);
            this.label26.TabIndex = 9;
            this.label26.Text = "分钟";
            // 
            // nudStepMinutes
            // 
            this.nudStepMinutes.Location = new System.Drawing.Point(530, 24);
            this.nudStepMinutes.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudStepMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStepMinutes.Name = "nudStepMinutes";
            this.nudStepMinutes.Size = new System.Drawing.Size(50, 21);
            this.nudStepMinutes.TabIndex = 8;
            this.nudStepMinutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStepMinutes.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(507, 27);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 7;
            this.label24.Text = "每";
            // 
            // cbSunday
            // 
            this.cbSunday.AutoSize = true;
            this.cbSunday.Location = new System.Drawing.Point(462, 64);
            this.cbSunday.Name = "cbSunday";
            this.cbSunday.Size = new System.Drawing.Size(60, 16);
            this.cbSunday.TabIndex = 6;
            this.cbSunday.Text = "星期日";
            this.cbSunday.UseVisualStyleBackColor = true;
            this.cbSunday.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cbThursday
            // 
            this.cbThursday.AutoSize = true;
            this.cbThursday.Location = new System.Drawing.Point(264, 64);
            this.cbThursday.Name = "cbThursday";
            this.cbThursday.Size = new System.Drawing.Size(60, 16);
            this.cbThursday.TabIndex = 6;
            this.cbThursday.Text = "星期四";
            this.cbThursday.UseVisualStyleBackColor = true;
            this.cbThursday.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // dtRepeatTime
            // 
            this.dtRepeatTime.CustomFormat = "HH:mm:ss";
            this.dtRepeatTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtRepeatTime.Location = new System.Drawing.Point(379, 25);
            this.dtRepeatTime.Name = "dtRepeatTime";
            this.dtRepeatTime.ShowUpDown = true;
            this.dtRepeatTime.Size = new System.Drawing.Size(121, 21);
            this.dtRepeatTime.TabIndex = 1;
            this.dtRepeatTime.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cbTuesday
            // 
            this.cbTuesday.AutoSize = true;
            this.cbTuesday.Location = new System.Drawing.Point(132, 64);
            this.cbTuesday.Name = "cbTuesday";
            this.cbTuesday.Size = new System.Drawing.Size(60, 16);
            this.cbTuesday.TabIndex = 6;
            this.cbTuesday.Text = "星期二";
            this.cbTuesday.UseVisualStyleBackColor = true;
            this.cbTuesday.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cbSaturday
            // 
            this.cbSaturday.AutoSize = true;
            this.cbSaturday.Location = new System.Drawing.Point(396, 64);
            this.cbSaturday.Name = "cbSaturday";
            this.cbSaturday.Size = new System.Drawing.Size(60, 16);
            this.cbSaturday.TabIndex = 6;
            this.cbSaturday.Text = "星期六";
            this.cbSaturday.UseVisualStyleBackColor = true;
            this.cbSaturday.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cbFriday
            // 
            this.cbFriday.AutoSize = true;
            this.cbFriday.Location = new System.Drawing.Point(330, 64);
            this.cbFriday.Name = "cbFriday";
            this.cbFriday.Size = new System.Drawing.Size(60, 16);
            this.cbFriday.TabIndex = 6;
            this.cbFriday.Text = "星期五";
            this.cbFriday.UseVisualStyleBackColor = true;
            this.cbFriday.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cbWednesday
            // 
            this.cbWednesday.AutoSize = true;
            this.cbWednesday.Location = new System.Drawing.Point(198, 64);
            this.cbWednesday.Name = "cbWednesday";
            this.cbWednesday.Size = new System.Drawing.Size(60, 16);
            this.cbWednesday.TabIndex = 6;
            this.cbWednesday.Text = "星期三";
            this.cbWednesday.UseVisualStyleBackColor = true;
            this.cbWednesday.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cbMonday
            // 
            this.cbMonday.AutoSize = true;
            this.cbMonday.Location = new System.Drawing.Point(66, 64);
            this.cbMonday.Name = "cbMonday";
            this.cbMonday.Size = new System.Drawing.Size(60, 16);
            this.cbMonday.TabIndex = 6;
            this.cbMonday.Text = "星期一";
            this.cbMonday.UseVisualStyleBackColor = true;
            this.cbMonday.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // nudRepeatStep
            // 
            this.nudRepeatStep.Location = new System.Drawing.Point(265, 25);
            this.nudRepeatStep.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudRepeatStep.Name = "nudRepeatStep";
            this.nudRepeatStep.Size = new System.Drawing.Size(61, 21);
            this.nudRepeatStep.TabIndex = 5;
            this.nudRepeatStep.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // lblStepUnit
            // 
            this.lblStepUnit.AutoSize = true;
            this.lblStepUnit.Location = new System.Drawing.Point(336, 29);
            this.lblStepUnit.Name = "lblStepUnit";
            this.lblStepUnit.Size = new System.Drawing.Size(17, 12);
            this.lblStepUnit.TabIndex = 4;
            this.lblStepUnit.Text = "天";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(206, 29);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 3;
            this.label23.Text = "执行间隔";
            // 
            // cbRepeatRate
            // 
            this.cbRepeatRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRepeatRate.FormattingEnabled = true;
            this.cbRepeatRate.Items.AddRange(new object[] {
            "每天",
            "每周"});
            this.cbRepeatRate.Location = new System.Drawing.Point(66, 26);
            this.cbRepeatRate.Name = "cbRepeatRate";
            this.cbRepeatRate.Size = new System.Drawing.Size(121, 20);
            this.cbRepeatRate.TabIndex = 2;
            this.cbRepeatRate.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(27, 29);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 12);
            this.label22.TabIndex = 1;
            this.label22.Text = "执行";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(4, 4);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 0;
            this.label21.Text = "频率";
            // 
            // pnlOnce
            // 
            this.pnlOnce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOnce.Controls.Add(this.label20);
            this.pnlOnce.Controls.Add(this.label19);
            this.pnlOnce.Controls.Add(this.dtOnceTime);
            this.pnlOnce.Controls.Add(this.dtOnceDate);
            this.pnlOnce.Controls.Add(this.label18);
            this.pnlOnce.Location = new System.Drawing.Point(188, 107);
            this.pnlOnce.Name = "pnlOnce";
            this.pnlOnce.Size = new System.Drawing.Size(684, 66);
            this.pnlOnce.TabIndex = 9;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(206, 32);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 2;
            this.label20.Text = "时间";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(28, 32);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 12);
            this.label19.TabIndex = 2;
            this.label19.Text = "日期";
            // 
            // dtOnceTime
            // 
            this.dtOnceTime.CustomFormat = "HH:mm:ss";
            this.dtOnceTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtOnceTime.Location = new System.Drawing.Point(269, 26);
            this.dtOnceTime.Name = "dtOnceTime";
            this.dtOnceTime.ShowUpDown = true;
            this.dtOnceTime.Size = new System.Drawing.Size(121, 21);
            this.dtOnceTime.TabIndex = 1;
            this.dtOnceTime.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // dtOnceDate
            // 
            this.dtOnceDate.CustomFormat = "yyyy-MM-dd";
            this.dtOnceDate.Location = new System.Drawing.Point(66, 26);
            this.dtOnceDate.Name = "dtOnceDate";
            this.dtOnceDate.Size = new System.Drawing.Size(121, 21);
            this.dtOnceDate.TabIndex = 1;
            this.dtOnceDate.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(4, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 0;
            this.label18.Text = "执行一次";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(636, 32);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 8;
            this.label17.Text = "计划类型";
            // 
            // cbPlanType
            // 
            this.cbPlanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanType.FormattingEnabled = true;
            this.cbPlanType.Items.AddRange(new object[] {
            "执行一次",
            "重复执行"});
            this.cbPlanType.Location = new System.Drawing.Point(695, 29);
            this.cbPlanType.Name = "cbPlanType";
            this.cbPlanType.Size = new System.Drawing.Size(121, 20);
            this.cbPlanType.TabIndex = 7;
            this.cbPlanType.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cbEnabled
            // 
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Location = new System.Drawing.Point(568, 31);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(48, 16);
            this.cbEnabled.TabIndex = 6;
            this.cbEnabled.Text = "启用";
            this.cbEnabled.UseVisualStyleBackColor = true;
            this.cbEnabled.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // txtPlanName
            // 
            this.txtPlanName.Location = new System.Drawing.Point(245, 29);
            this.txtPlanName.Name = "txtPlanName";
            this.txtPlanName.Size = new System.Drawing.Size(317, 21);
            this.txtPlanName.TabIndex = 5;
            this.txtPlanName.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(186, 35);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 4;
            this.label16.Text = "计划名称";
            // 
            // btnSavePlan
            // 
            this.btnSavePlan.Location = new System.Drawing.Point(90, 603);
            this.btnSavePlan.Name = "btnSavePlan";
            this.btnSavePlan.Size = new System.Drawing.Size(75, 23);
            this.btnSavePlan.TabIndex = 3;
            this.btnSavePlan.Text = "保存修改";
            this.btnSavePlan.UseVisualStyleBackColor = true;
            this.btnSavePlan.Click += new System.EventHandler(this.btnSavePlan_Click);
            // 
            // btnRemovePlan
            // 
            this.btnRemovePlan.Location = new System.Drawing.Point(90, 573);
            this.btnRemovePlan.Name = "btnRemovePlan";
            this.btnRemovePlan.Size = new System.Drawing.Size(75, 23);
            this.btnRemovePlan.TabIndex = 2;
            this.btnRemovePlan.Text = "删除计划";
            this.btnRemovePlan.UseVisualStyleBackColor = true;
            this.btnRemovePlan.Click += new System.EventHandler(this.btnRemovePlan_Click);
            // 
            // btnNewPlan
            // 
            this.btnNewPlan.Location = new System.Drawing.Point(11, 573);
            this.btnNewPlan.Name = "btnNewPlan";
            this.btnNewPlan.Size = new System.Drawing.Size(75, 23);
            this.btnNewPlan.TabIndex = 2;
            this.btnNewPlan.Text = "新建计划";
            this.btnNewPlan.UseVisualStyleBackColor = true;
            this.btnNewPlan.Click += new System.EventHandler(this.btnNewPlan_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "计划列表";
            // 
            // svc
            // 
            this.svc.ServiceName = "DBSyncService";
            // 
            // var_name
            // 
            this.var_name.DataPropertyName = "var_name";
            this.var_name.HeaderText = "变量名";
            this.var_name.Name = "var_name";
            // 
            // var_type
            // 
            this.var_type.DataPropertyName = "var_type";
            this.var_type.DataSource = this.cdsConfig;
            this.var_type.DisplayMember = "zd_var_type.type_name";
            this.var_type.HeaderText = "变量类型";
            this.var_type.Name = "var_type";
            this.var_type.ValueMember = "zd_var_type.type_id";
            // 
            // var_value
            // 
            this.var_value.DataPropertyName = "var_value";
            this.var_value.HeaderText = "变量值";
            this.var_value.Name = "var_value";
            this.var_value.Width = 400;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 718);
            this.Controls.Add(this.mainTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据同步工具";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mainTab.ResumeLayout(false);
            this.tLog.ResumeLayout(false);
            this.tLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).EndInit();
            this.tSrc.ResumeLayout(false);
            this.tSrc.PerformLayout();
            this.tTarget.ResumeLayout(false);
            this.tTarget.PerformLayout();
            this.tSQL.ResumeLayout(false);
            this.tSQL.PerformLayout();
            this.tSQLTest.ResumeLayout(false);
            this.tSQLTest.PerformLayout();
            this.tabResult.ResumeLayout(false);
            this.tabGrid.ResumeLayout(false);
            this.scGrid.Panel1.ResumeLayout(false);
            this.scGrid.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scGrid)).EndInit();
            this.scGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTest)).EndInit();
            this.tabMsg.ResumeLayout(false);
            this.tabMsg.PerformLayout();
            this.tConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbgrdconfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdsConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.global_var)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zd_var_type)).EndInit();
            this.tPlan.ResumeLayout(false);
            this.tPlan.PerformLayout();
            this.pnlStep.ResumeLayout(false);
            this.pnlStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSqlSteps)).EndInit();
            this.pnlRate.ResumeLayout(false);
            this.pnlRate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStepMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatStep)).EndInit();
            this.pnlOnce.ResumeLayout(false);
            this.pnlOnce.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage tLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.DataGridView gridLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn coldate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSrc;
        private System.Windows.Forms.DateTimePicker dtpLog;
        private System.Windows.Forms.Button btnRLog;
        private System.Windows.Forms.TabPage tSrc;
        private System.Windows.Forms.TabPage tTarget;
        private System.Windows.Forms.TabPage tSQL;
        private System.Windows.Forms.TabPage tSQLTest;
        private System.Windows.Forms.TextBox txtSrcPassword;
        private System.Windows.Forms.TextBox txtSrcUser;
        private System.Windows.Forms.TextBox txtSrcPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSrcSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSrcAuthz;
        private System.Windows.Forms.ComboBox cbSrcType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSrcDB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSrcApply;
        private System.Windows.Forms.Button btnDstApply;
        private System.Windows.Forms.TextBox txtDstDB;
        private System.Windows.Forms.TextBox txtDstPassword;
        private System.Windows.Forms.TextBox txtDstUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDstPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDstSource;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbDstAuthz;
        private System.Windows.Forms.ComboBox cbDstType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTestSQL;
        private System.Windows.Forms.RadioButton rbDst;
        private System.Windows.Forms.RadioButton rbSrc;
        private System.Windows.Forms.CheckBox cbTestDebug;
        private System.Windows.Forms.Button btnTestExec;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage tabGrid;
        private System.Windows.Forms.DataGridView gridTest;
        private System.Windows.Forms.TabPage tabMsg;
        private System.Windows.Forms.SplitContainer scGrid;
        private System.Windows.Forms.ListBox lbGrid;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rbSQLDst;
        private System.Windows.Forms.RadioButton rbSQLSrc;
        private System.Windows.Forms.ComboBox cbSQLSelect;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.Button btnNewFile;
        private System.Windows.Forms.TextBox txtNewFile;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDstTest;
        private System.Windows.Forms.Button btnSrcTest;
        private System.Windows.Forms.TabPage tConfig;
        private System.Windows.Forms.DataGridView dbgrdconfig;
        private System.Data.DataSet cdsConfig;
        private System.Data.DataTable global_var;
        private System.Data.DataColumn var_name_col;
        private System.Data.DataColumn var_type_col;
        private System.Data.DataColumn var_value_col;
        private System.Data.DataTable zd_var_type;
        private System.Data.DataColumn type_id_col;
        private System.Data.DataColumn type_name_col;
        private System.Windows.Forms.TabPage tPlan;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnRemovePlan;
        private System.Windows.Forms.Button btnNewPlan;
        private System.Windows.Forms.Button btnSavePlan;
        private System.ServiceProcess.ServiceController svc;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbPlanType;
        private System.Windows.Forms.CheckBox cbEnabled;
        private System.Windows.Forms.TextBox txtPlanName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlOnce;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtOnceTime;
        private System.Windows.Forms.DateTimePicker dtOnceDate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlRate;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbRepeatRate;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel pnlStep;
        private System.Windows.Forms.DataGridView dgvSqlSteps;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.CheckBox cbSunday;
        private System.Windows.Forms.CheckBox cbThursday;
        private System.Windows.Forms.CheckBox cbTuesday;
        private System.Windows.Forms.CheckBox cbSaturday;
        private System.Windows.Forms.CheckBox cbFriday;
        private System.Windows.Forms.CheckBox cbWednesday;
        private System.Windows.Forms.CheckBox cbMonday;
        private System.Windows.Forms.NumericUpDown nudRepeatStep;
        private System.Windows.Forms.Label lblStepUnit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnTestPlan;
        private System.Windows.Forms.ListBox lvPlan;
        private System.Windows.Forms.DateTimePicker dtRepeatTime;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown nudStepMinutes;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnSaveStep;
        private System.Windows.Forms.Button btnRemoveStep;
        private System.Windows.Forms.Button btnNewStep;
        private System.Windows.Forms.Label lblLastSuccessTime;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanID_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanDataID_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanDataName_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanSql_col;
        private System.Windows.Forms.DataGridViewComboBoxColumn FailMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn var_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn var_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn var_value;
    }
}

