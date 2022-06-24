namespace AutoPPPoE
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.niPermanent = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIPTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPause = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelAdapter = new System.Windows.Forms.Label();
            this.cbAdapter = new System.Windows.Forms.ComboBox();
            this.labelName = new System.Windows.Forms.Label();
            this.cbName = new System.Windows.Forms.ComboBox();
            this.labelAccount = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.labelFastPing = new System.Windows.Forms.Label();
            this.numFastPing = new System.Windows.Forms.NumericUpDown();
            this.labelSlowPing = new System.Windows.Forms.Label();
            this.numSlowPing = new System.Windows.Forms.NumericUpDown();
            this.chkAutomaticStart = new System.Windows.Forms.CheckBox();
            this.labelAutomaticStartWait = new System.Windows.Forms.Label();
            this.labelAutomaticStartWaitUnit = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.numAutomaticStartWait = new System.Windows.Forms.NumericUpDown();
            this.labelFastPingUnit = new System.Windows.Forms.Label();
            this.labelSlowPingUnit = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.labelSetting = new System.Windows.Forms.Label();
            this.cbSetting = new System.Windows.Forms.ComboBox();
            this.btnManageSetting = new System.Windows.Forms.Button();
            this.chkShowDebugLog = new System.Windows.Forms.CheckBox();
            this.labelDebugLog = new System.Windows.Forms.Label();
            this.txtDebugLog = new System.Windows.Forms.TextBox();
            this.cmsRightClickMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFastPing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSlowPing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAutomaticStartWait)).BeginInit();
            this.SuspendLayout();
            // 
            // niPermanent
            // 
            this.niPermanent.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.niPermanent.BalloonTipTitle = "自動撥號程式";
            this.niPermanent.ContextMenuStrip = this.cmsRightClickMenu;
            this.niPermanent.Icon = ((System.Drawing.Icon)(resources.GetObject("niPermanent.Icon")));
            this.niPermanent.Text = "自動撥號程式";
            this.niPermanent.DoubleClick += new System.EventHandler(this.niPermanent_DoubleClick);
            // 
            // cmsRightClickMenu
            // 
            this.cmsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetting,
            this.tsmiIPTool,
            this.tsmiPause,
            this.tsmiExit});
            this.cmsRightClickMenu.Name = "cmsRightClickMenu";
            this.cmsRightClickMenu.Size = new System.Drawing.Size(114, 92);
            // 
            // tsmiSetting
            // 
            this.tsmiSetting.Name = "tsmiSetting";
            this.tsmiSetting.Size = new System.Drawing.Size(113, 22);
            this.tsmiSetting.Text = "設定";
            this.tsmiSetting.Click += new System.EventHandler(this.tsmiSetting_Click);
            // 
            // tsmiIPTool
            // 
            this.tsmiIPTool.Name = "tsmiIPTool";
            this.tsmiIPTool.Size = new System.Drawing.Size(113, 22);
            this.tsmiIPTool.Text = "IP 工具";
            this.tsmiIPTool.Click += new System.EventHandler(this.tsmiIPTool_Click);
            // 
            // tsmiPause
            // 
            this.tsmiPause.Name = "tsmiPause";
            this.tsmiPause.Size = new System.Drawing.Size(113, 22);
            this.tsmiPause.Text = "暫停";
            this.tsmiPause.Click += new System.EventHandler(this.tsmiPause_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(113, 22);
            this.tsmiExit.Text = "結束";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelTitle.Location = new System.Drawing.Point(12, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(105, 20);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "自動撥號程式";
            // 
            // labelAdapter
            // 
            this.labelAdapter.AutoSize = true;
            this.labelAdapter.Location = new System.Drawing.Point(25, 80);
            this.labelAdapter.Name = "labelAdapter";
            this.labelAdapter.Size = new System.Drawing.Size(67, 13);
            this.labelAdapter.TabIndex = 5;
            this.labelAdapter.Text = "網路卡名稱";
            // 
            // cbAdapter
            // 
            this.cbAdapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAdapter.Enabled = false;
            this.cbAdapter.FormattingEnabled = true;
            this.cbAdapter.Location = new System.Drawing.Point(96, 77);
            this.cbAdapter.Name = "cbAdapter";
            this.cbAdapter.Size = new System.Drawing.Size(260, 21);
            this.cbAdapter.TabIndex = 6;
            this.cbAdapter.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(25, 108);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(104, 13);
            this.labelName.TabIndex = 7;
            this.labelName.Text = "PPPoE 介面卡名稱";
            // 
            // cbName
            // 
            this.cbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbName.Enabled = false;
            this.cbName.FormattingEnabled = true;
            this.cbName.Location = new System.Drawing.Point(130, 105);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(226, 21);
            this.cbName.TabIndex = 8;
            this.cbName.TabStop = false;
            // 
            // labelAccount
            // 
            this.labelAccount.AutoSize = true;
            this.labelAccount.Location = new System.Drawing.Point(25, 137);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(31, 13);
            this.labelAccount.TabIndex = 9;
            this.labelAccount.Text = "帳號";
            // 
            // txtAccount
            // 
            this.txtAccount.Enabled = false;
            this.txtAccount.Location = new System.Drawing.Point(60, 133);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(296, 20);
            this.txtAccount.TabIndex = 10;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(25, 167);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(31, 13);
            this.labelPassword.TabIndex = 11;
            this.labelPassword.Text = "密碼";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(60, 164);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(296, 20);
            this.txtPassword.TabIndex = 12;
            // 
            // labelFastPing
            // 
            this.labelFastPing.AutoSize = true;
            this.labelFastPing.Location = new System.Drawing.Point(25, 196);
            this.labelFastPing.Name = "labelFastPing";
            this.labelFastPing.Size = new System.Drawing.Size(106, 13);
            this.labelFastPing.TabIndex = 14;
            this.labelFastPing.Text = "快速 Ping 等待時間";
            // 
            // numFastPing
            // 
            this.numFastPing.Enabled = false;
            this.numFastPing.Location = new System.Drawing.Point(135, 194);
            this.numFastPing.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.numFastPing.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFastPing.Name = "numFastPing";
            this.numFastPing.Size = new System.Drawing.Size(221, 20);
            this.numFastPing.TabIndex = 15;
            this.numFastPing.Value = new decimal(new int[] {
            750,
            0,
            0,
            0});
            // 
            // labelSlowPing
            // 
            this.labelSlowPing.AutoSize = true;
            this.labelSlowPing.Location = new System.Drawing.Point(25, 226);
            this.labelSlowPing.Name = "labelSlowPing";
            this.labelSlowPing.Size = new System.Drawing.Size(106, 13);
            this.labelSlowPing.TabIndex = 17;
            this.labelSlowPing.Text = "慢速 Ping 等待時間";
            // 
            // numSlowPing
            // 
            this.numSlowPing.Enabled = false;
            this.numSlowPing.Location = new System.Drawing.Point(135, 224);
            this.numSlowPing.Maximum = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
            this.numSlowPing.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSlowPing.Name = "numSlowPing";
            this.numSlowPing.Size = new System.Drawing.Size(221, 20);
            this.numSlowPing.TabIndex = 18;
            this.numSlowPing.Value = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            // 
            // chkAutomaticStart
            // 
            this.chkAutomaticStart.AutoSize = true;
            this.chkAutomaticStart.Enabled = false;
            this.chkAutomaticStart.Location = new System.Drawing.Point(27, 267);
            this.chkAutomaticStart.Name = "chkAutomaticStart";
            this.chkAutomaticStart.Size = new System.Drawing.Size(122, 17);
            this.chkAutomaticStart.TabIndex = 20;
            this.chkAutomaticStart.Text = "程序启动自动拨号";
            this.chkAutomaticStart.UseVisualStyleBackColor = true;
            this.chkAutomaticStart.CheckedChanged += new System.EventHandler(this.chkAutomaticStart_CheckedChanged);
            // 
            // labelAutomaticStartWait
            // 
            this.labelAutomaticStartWait.AutoSize = true;
            this.labelAutomaticStartWait.Location = new System.Drawing.Point(25, 293);
            this.labelAutomaticStartWait.Name = "labelAutomaticStartWait";
            this.labelAutomaticStartWait.Size = new System.Drawing.Size(79, 13);
            this.labelAutomaticStartWait.TabIndex = 21;
            this.labelAutomaticStartWait.Text = "啟動等待時間";
            // 
            // labelAutomaticStartWaitUnit
            // 
            this.labelAutomaticStartWaitUnit.AutoSize = true;
            this.labelAutomaticStartWaitUnit.Location = new System.Drawing.Point(377, 293);
            this.labelAutomaticStartWaitUnit.Name = "labelAutomaticStartWaitUnit";
            this.labelAutomaticStartWaitUnit.Size = new System.Drawing.Size(19, 13);
            this.labelAutomaticStartWaitUnit.TabIndex = 23;
            this.labelAutomaticStartWaitUnit.Text = "秒";
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(16, 332);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(406, 25);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "儲存設定";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // numAutomaticStartWait
            // 
            this.numAutomaticStartWait.Enabled = false;
            this.numAutomaticStartWait.Location = new System.Drawing.Point(135, 290);
            this.numAutomaticStartWait.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numAutomaticStartWait.Name = "numAutomaticStartWait";
            this.numAutomaticStartWait.Size = new System.Drawing.Size(221, 20);
            this.numAutomaticStartWait.TabIndex = 22;
            this.numAutomaticStartWait.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // labelFastPingUnit
            // 
            this.labelFastPingUnit.AutoSize = true;
            this.labelFastPingUnit.Location = new System.Drawing.Point(377, 196);
            this.labelFastPingUnit.Name = "labelFastPingUnit";
            this.labelFastPingUnit.Size = new System.Drawing.Size(31, 13);
            this.labelFastPingUnit.TabIndex = 16;
            this.labelFastPingUnit.Text = "豪秒";
            // 
            // labelSlowPingUnit
            // 
            this.labelSlowPingUnit.AutoSize = true;
            this.labelSlowPingUnit.Location = new System.Drawing.Point(377, 226);
            this.labelSlowPingUnit.Name = "labelSlowPingUnit";
            this.labelSlowPingUnit.Size = new System.Drawing.Size(31, 13);
            this.labelSlowPingUnit.TabIndex = 19;
            this.labelSlowPingUnit.Text = "豪秒";
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(16, 363);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(406, 25);
            this.btnStart.TabIndex = 25;
            this.btnStart.Text = "開始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Enabled = false;
            this.chkShowPassword.Location = new System.Drawing.Point(377, 166);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(50, 17);
            this.chkShowPassword.TabIndex = 13;
            this.chkShowPassword.Text = "顯示";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // labelSetting
            // 
            this.labelSetting.AutoSize = true;
            this.labelSetting.Location = new System.Drawing.Point(25, 52);
            this.labelSetting.Name = "labelSetting";
            this.labelSetting.Size = new System.Drawing.Size(55, 13);
            this.labelSetting.TabIndex = 2;
            this.labelSetting.Text = "設定名稱";
            // 
            // cbSetting
            // 
            this.cbSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSetting.FormattingEnabled = true;
            this.cbSetting.Location = new System.Drawing.Point(84, 49);
            this.cbSetting.Name = "cbSetting";
            this.cbSetting.Size = new System.Drawing.Size(272, 21);
            this.cbSetting.TabIndex = 3;
            this.cbSetting.TabStop = false;
            this.cbSetting.SelectedIndexChanged += new System.EventHandler(this.cbSetting_SelectedIndexChanged);
            // 
            // btnManageSetting
            // 
            this.btnManageSetting.Location = new System.Drawing.Point(377, 47);
            this.btnManageSetting.Name = "btnManageSetting";
            this.btnManageSetting.Size = new System.Drawing.Size(45, 25);
            this.btnManageSetting.TabIndex = 4;
            this.btnManageSetting.Text = "管理";
            this.btnManageSetting.UseVisualStyleBackColor = true;
            this.btnManageSetting.Click += new System.EventHandler(this.btnManageSetting_Click);
            // 
            // chkShowDebugLog
            // 
            this.chkShowDebugLog.AutoSize = true;
            this.chkShowDebugLog.Location = new System.Drawing.Point(210, 15);
            this.chkShowDebugLog.Name = "chkShowDebugLog";
            this.chkShowDebugLog.Size = new System.Drawing.Size(98, 17);
            this.chkShowDebugLog.TabIndex = 1;
            this.chkShowDebugLog.Text = "顯示除錯訊息";
            this.chkShowDebugLog.UseVisualStyleBackColor = true;
            this.chkShowDebugLog.CheckedChanged += new System.EventHandler(this.chkShowDebugLog_CheckedChanged);
            // 
            // labelDebugLog
            // 
            this.labelDebugLog.AutoSize = true;
            this.labelDebugLog.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelDebugLog.Location = new System.Drawing.Point(470, 10);
            this.labelDebugLog.Name = "labelDebugLog";
            this.labelDebugLog.Size = new System.Drawing.Size(73, 20);
            this.labelDebugLog.TabIndex = 26;
            this.labelDebugLog.Text = "除錯輸出";
            this.labelDebugLog.Visible = false;
            // 
            // txtDebugLog
            // 
            this.txtDebugLog.Location = new System.Drawing.Point(485, 47);
            this.txtDebugLog.Multiline = true;
            this.txtDebugLog.Name = "txtDebugLog";
            this.txtDebugLog.ReadOnly = true;
            this.txtDebugLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDebugLog.Size = new System.Drawing.Size(230, 342);
            this.txtDebugLog.TabIndex = 27;
            this.txtDebugLog.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 453);
            this.Controls.Add(this.txtDebugLog);
            this.Controls.Add(this.labelDebugLog);
            this.Controls.Add(this.chkShowDebugLog);
            this.Controls.Add(this.btnManageSetting);
            this.Controls.Add(this.cbSetting);
            this.Controls.Add(this.labelSetting);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.labelSlowPingUnit);
            this.Controls.Add(this.labelFastPingUnit);
            this.Controls.Add(this.numAutomaticStartWait);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelAutomaticStartWaitUnit);
            this.Controls.Add(this.labelAutomaticStartWait);
            this.Controls.Add(this.chkAutomaticStart);
            this.Controls.Add(this.numSlowPing);
            this.Controls.Add(this.labelSlowPing);
            this.Controls.Add(this.numFastPing);
            this.Controls.Add(this.labelFastPing);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.labelAccount);
            this.Controls.Add(this.cbName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.cbAdapter);
            this.Controls.Add(this.labelAdapter);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "自動撥號程式";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.cmsRightClickMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numFastPing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSlowPing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAutomaticStartWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon niPermanent;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelAdapter;
        private System.Windows.Forms.ComboBox cbAdapter;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ComboBox cbName;
        private System.Windows.Forms.Label labelAccount;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label labelFastPing;
        private System.Windows.Forms.NumericUpDown numFastPing;
        private System.Windows.Forms.Label labelSlowPing;
        private System.Windows.Forms.NumericUpDown numSlowPing;
        private System.Windows.Forms.CheckBox chkAutomaticStart;
        private System.Windows.Forms.Label labelAutomaticStartWait;
        private System.Windows.Forms.Label labelAutomaticStartWaitUnit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown numAutomaticStartWait;
        private System.Windows.Forms.Label labelFastPingUnit;
        private System.Windows.Forms.Label labelSlowPingUnit;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ContextMenuStrip cmsRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiPause;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiIPTool;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Label labelSetting;
        private System.Windows.Forms.Button btnManageSetting;
        public System.Windows.Forms.ComboBox cbSetting;
        private System.Windows.Forms.CheckBox chkShowDebugLog;
        private System.Windows.Forms.Label labelDebugLog;
        private System.Windows.Forms.TextBox txtDebugLog;
    }
}

