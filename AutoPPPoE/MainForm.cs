using DotRas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPPPoE
{
    public partial class MainForm : Form
    {
        public enum WinSWStatus
        {
            NonExistent,
            Started,
            Stopped,
        }

        private static readonly IDictionary<Status, string> TIP_MESSAGE = new Dictionary<Status, string>()
        {
            { Status.SHOW_WELCOME, "本程序将常驻右下托盘" },
            { Status.SHOW_START, "正在运行 ..." },
            { Status.SHOW_AUTOMATIC_START, "已自动开启拨号" },
            { Status.SHOW_DISCONNECT, "监测到断网了，正在重拨 ..." },
            { Status.SHOW_ADAPTER, "目标网卡已停用 正在启用 ..." },
            { Status.SHOW_CONNECT, "正在 PPPoE 拨号 ..." },
            { Status.SHOW_AUTOMATIC_REDIAL_DUE_TO_TIMEOUT, "由于PPPoE连接时间过长 正在重拨 PPPoE ..." },
            { Status.SHOW_AUTOMATIC_REDIAL_DUE_TO_NO_INTERNET, "由于无法访问互联网 正在重拨 PPPoE ..." },
            { Status.SHOW_NO_AUTOMATIC_STARTUP_SERVICE_WINSW, "自动启动服务文件不存在，系统启动时无法自动拨号" }
        };

        private static readonly Config config = Program.config;
        private Thread checkThread;

        public MainForm()
        {
            InitializeComponent();
        }

        public void InitLoad()
        {
            loadNetworkInterface();
            Util.loadSettingNameUI(cbSetting);
            updateUIStatus();
            txtAccount.SelectionStart = txtAccount.Text.Length;
        }

        private void loadNetworkInterface()
        {
            AdapterManager adapter = Program.adapter;
            foreach (string adapterName in adapter.adapterName)
            {
                cbAdapter.Items.Add(adapterName);
            }

            cbAdapter.SelectedIndex = 0;

            foreach (string rasName in adapter.rasName)
            {
                cbName.Items.Add(rasName);
            }

            cbName.SelectedIndex = 0;
        }

        private void enableControlUI(bool enable)
        {
            chkAutomaticStartOnSystemBoot.Enabled =
                cbAdapter.Enabled = cbName.Enabled = txtAccount.Enabled
                    = txtPassword.Enabled = chkShowPassword.Enabled = numTcpPing.Enabled
                        = txtTcpPingHost.Enabled = txtTcpPingPort.Enabled
                            = chkAutomaticStart.Enabled =
                                numAutomaticStartWait.Enabled
                                    = numAutomaticRedialTimeoutMinutes.Enabled
                                        = btnSave.Enabled = enable;
            if (enable)
            {
                updateAutomaticStartWaitUI();
            }
        }

        private void restoreDefault()
        {
            cbAdapter.SelectedIndex               = cbName.SelectedIndex = 0;
            txtAccount.Text                       = txtPassword.Text     = string.Empty;
            chkShowPassword.Checked               = false;
            chkAutomaticStart.Checked             = Constant.DEFAULT_AUTOMATIC_START;
            chkAutomaticStartOnSystemBoot.Checked = Constant.DEFAULT_AUTOMATIC_START_ON_SYSTEM_BOOT;
            Util.forceUpdateNumericUpDownValue(numTcpPing, Constant.DEFAULT_TCP_PING_WAIT_TIME);
            txtTcpPingHost.Text = Constant.DEFAULT_TCP_PING_CHECK_HOST;
            txtTcpPingPort.Text = Constant.DEFAULT_TCP_PING_CHECK_PORT.ToString();
            Util.forceUpdateNumericUpDownValue(numAutomaticStartWait, Constant.DEFAULT_AUTOMATIC_START_WAIT_TIME);
            Util.forceUpdateNumericUpDownValue(numAutomaticRedialTimeoutMinutes,
                Constant.DEFAULT_AUTOMATIC_REDIAL_TIMEOUT_MINUTES);
        }

        private void updateUISetting()
        {
            Setting current = config.current;
            Util.optionSelect(cbAdapter, current.adapter);
            Util.optionSelect(cbName, current.name);

            txtAccount.Text                       = current.account;
            txtPassword.Text                      = current.plainTextPassword;
            chkAutomaticStart.Checked             = current.automaticStart;
            chkAutomaticStartOnSystemBoot.Checked = current.automaticStartOnSystemBoot;
            Util.forceUpdateNumericUpDownValue(numTcpPing, current.tcpPing);
            txtTcpPingHost.Text = current.tcpPingHost;
            txtTcpPingPort.Text = current.tcpPingPort.ToString();
            Util.forceUpdateNumericUpDownValue(numAutomaticStartWait, current.automaticStartWaitTime);
            Util.forceUpdateNumericUpDownValue(numAutomaticRedialTimeoutMinutes, current.automaticRedialTimeoutMinutes);

            if (!Util.CanUseService())
            {
                showBalloonTip(Status.SHOW_NO_AUTOMATIC_STARTUP_SERVICE_WINSW, false);
                // 允许用户取消，取消后不允许再修改
                if (chkAutomaticStartOnSystemBoot.Checked)
                {
                    chkAutomaticStartOnSystemBoot.Enabled = true;
                }
                else
                {
                    chkAutomaticStartOnSystemBoot.Enabled = false;
                }
            }
        }

        private void updateUIStatus()
        {
            bool nonServiceRunning = checkThread != null;

            if (nonServiceRunning)
            {
                // running
                cbSetting.Enabled = btnManageSetting.Enabled = false;
                enableControlUI(false);
                updateUISetting();
                btnStart.Text = "停止";
            }
            else
            {
                // stopped
                cbSetting.Enabled = btnManageSetting.Enabled = true;
                bool hasSelect                               = config.select != null;
                enableControlUI(hasSelect);
                btnStart.Enabled = hasSelect;
                if (!hasSelect)
                {
                    restoreDefault();
                }
                else
                {
                    updateUISetting();
                }

                btnStart.Text = "开始";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            welcome();
            if (config.select != null && config.current.automaticStart)
            {
                if (applyUIStart(true))
                {
                    startPPPoEThread(Status.MODE_START_UP);
                    updateUIStatus();
                }
            }
        }

        private static bool isPPPoEActive()
        {
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                if (connection.EntryName == config.current.name)
                {
                    return true;
                }
            }

            return false;
        }

        private static TimeSpan? getPPPoEActiveConnectionDuration()
        {
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                if (connection.EntryName == config.current.name)
                {
                    return connection.GetConnectionStatistics().ConnectionDuration;
                }
            }

            return null;
        }

        private static bool isBussinessWorkingHour()
        {
            var now = DateTime.Now;
            var dt1 = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            var dt2 = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0);
            var dt3 = new DateTime(now.Year, now.Month, now.Day, 13, 0, 0);
            var dt4 = new DateTime(now.Year, now.Month, now.Day, 17, 0, 0);
            if (DateTime.Now >= dt1 && DateTime.Now <= dt2) return true;
            if (DateTime.Now >= dt3 && DateTime.Now <= dt4) return true;
            return false;
        }

        private Setting createSetting()
        {
            return new Setting(cbAdapter.SelectedItem.ToString(),
                cbName.SelectedItem.ToString(),
                txtAccount.Text,
                Util.EncryptPassword(txtPassword.Text),
                Convert.ToInt32(numTcpPing.Value),
                txtTcpPingHost.Text,
                Convert.ToInt32(txtTcpPingPort.Text),
                chkAutomaticStart.Checked,
                chkAutomaticStartOnSystemBoot.Checked,
                Convert.ToInt32(numAutomaticStartWait.Value),
                Convert.ToInt32(numAutomaticRedialTimeoutMinutes.Value));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            txtAccount.Text  = Util.removeWhiteSpace(txtAccount.Text);
            txtPassword.Text = Util.removeWhiteSpace(txtPassword.Text);
            try
            {
                Setting current = createSetting();
                config.current = current;
                config.saveSetting();
                updateUISetting();
                HandleServiceInstallation(current.automaticStartOnSystemBoot);
            }
            catch (Exception ex)
            {
                MessageBox.Show("储存设定发生异常 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private WinSWStatus CheckWinSWStatus()
        {
            var statusRet = CommandHelper.runCommandNoStdIn($@"""{Constant.winswServiceExePath}""", "status");
            statusRet = statusRet.Trim().TrimEnd('\r', '\n');
            var parsed = Enum.TryParse(statusRet, true, out WinSWStatus ret);
            if (parsed) return ret;
            throw new EWException($"invalid WinSW status ret {statusRet}");
        }

        private void HandleServiceInstallation(bool currentAutomaticStartOnSystemBoot)
        {
            if (!Util.CanUseService())
            {
                appendLog($"service PREPARE: winsw service exe [{Constant.winswServiceExePath}] not exists");
                return;
            }

            if (currentAutomaticStartOnSystemBoot)
            {
                var ret = CommandHelper.runCommand($@"""{Constant.winswServiceExePath}"" uninstall",
                    $@"""{Constant.winswServiceExePath}"" install");
                appendLog($"service INSTALL: {ret}");
            }
            else
            {
                var ret = CommandHelper.runCommand($@"""{Constant.winswServiceExePath}"" uninstall");
                appendLog($"service UNINSTALL: {ret}");
            }
        }

        private static bool isAdapterAlive()
        {
            foreach (string adapter in NetworkInterface.GetAllNetworkInterfaces().Select(target => target.Name))
            {
                if (adapter == Util.GetNicConnId(config.current.adapter))
                {
                    return true;
                }
            }

            return false;
        }

        private void startPPPoEThread(Status mode)
        {
            checkThread = new Thread(() => checkConnect(mode))
            {
                Priority = ThreadPriority.AboveNormal
            };
            checkThread.Start();
        }

        private void startPPPoE()
        {
            Setting current = config.current;
            var ret = CommandHelper.runCommand(
                $@"rasdial ""{current.name}"" {current.account} {current.plainTextPassword}");
            appendLog(ret);
        }

        private void stopPPPoE()
        {
            var ret = CommandHelper.runCommand($@"rasdial ""{config.current.name}"" /disconnect");
            appendLog(ret);
        }

        private void enableAdapter()
        {
            var currConfigNicConnId = Util.GetNicConnId(config.current.adapter);
            var ret = CommandHelper.runCommand($@"netsh interface set interface ""{currConfigNicConnId}"" enable");
            appendLog(ret);
        }

        private void welcome()
        {
            niPermanent.Visible = true;
            showBalloonTip(Status.SHOW_WELCOME);
        }

        private void showBalloonTip(Status mode, bool log = true)
        {
            string message = TIP_MESSAGE[mode];
            niPermanent.BalloonTipText = message;
            niPermanent.ShowBalloonTip(Constant.TOOL_TIP_SHOW_DURATION);
            if (log)
            {
                appendDesktopLog(message);
            }
        }

        private double TimeSpan2LoopCount(TimeSpan ts)
        {
            return Math.Ceiling(ts.TotalMilliseconds / Constant.WAIT_NEXT_TIME_DELAY);
        }

        private TimeSpan LoopCount2TimeSpan(int loopCount)
        {
            return TimeSpan.FromMilliseconds(loopCount * Constant.WAIT_NEXT_TIME_DELAY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int HowFastWeExpandTcpPingInterval()
        {
            // 工作时间避免拥堵，快速拉长
            return isBussinessWorkingHour() ? 4 : 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int HowFastWeShrinkTcpPingInterval()
        {
            // 非工作时间快速缩短
            return isBussinessWorkingHour() ? 4 : 8;
        }

        private void checkConnect(Status mode)
        {
            if (mode == Status.MODE_START_UP)
            {
                Thread.Sleep(config.current.automaticStartWaitTime * 1000);
            }

            var minTcpPingInterval                  = TimeSpan.FromSeconds(10);
            var minTcpPingIntervalNextTimeLoopCount = Convert.ToInt32(TimeSpan2LoopCount(minTcpPingInterval));
            var maxTcpPingInterval                  = TimeSpan.FromMinutes(8);
            var maxTcpPingIntervalNextTimeLoopCount = Convert.ToInt32(TimeSpan2LoopCount(maxTcpPingInterval));

            int currentTcpPingInterval = minTcpPingIntervalNextTimeLoopCount;
            int tcpPingCheckFailCount  = 0;
            int waitNextTimeLoopCount  = 0;

            appendLog("网络连通性监测正在进行...");
            while (true)
            {
                try
                {
                    if (!tsmiPause.Checked)
                    {
                        if (!isAdapterAlive())
                        {
                            showBalloonTip(Status.SHOW_ADAPTER, true);
                            enableAdapter();
                            Constant.wait(Status.WAIT_ADAPTER); // 等待網路卡就緒
                        }

                        if (!isPPPoEActive())
                        {
                            showBalloonTip(Status.SHOW_CONNECT, true);
                            startPPPoE();
                        }
                        else
                        {
                            Setting current = config.current;

                            try
                            {
                                checked
                                {
                                    ++waitNextTimeLoopCount;
                                }
                            }
                            catch (OverflowException)
                            {
                                waitNextTimeLoopCount = 0;
                            }

                            // 错误计数过大时加快检测
                            bool shouldDoTcpPingCheck = waitNextTimeLoopCount % currentTcpPingInterval == 0 ||
                                                        tcpPingCheckFailCount > Constant.MAX_TCP_PING_CHECK_ATTEMPT / 2;

                            if (shouldDoTcpPingCheck)
                            {
                                var restoredFromFailure = false;
                                var tcpingRet = PingHelper.pingHost(current.tcpPingHost, current.tcpPingPort,
                                    current.tcpPing);
                                if (tcpingRet)
                                {
                                    // success
                                    restoredFromFailure    =  tcpPingCheckFailCount > 0;
                                    tcpPingCheckFailCount  =  0;
                                    currentTcpPingInterval *= HowFastWeExpandTcpPingInterval();
                                }
                                else
                                {
                                    // fail
                                    ++tcpPingCheckFailCount;
                                    currentTcpPingInterval /= HowFastWeShrinkTcpPingInterval();
                                }

                                if (currentTcpPingInterval > maxTcpPingIntervalNextTimeLoopCount)
                                {
                                    currentTcpPingInterval = maxTcpPingIntervalNextTimeLoopCount;
                                }

                                if (currentTcpPingInterval < minTcpPingIntervalNextTimeLoopCount)
                                {
                                    currentTcpPingInterval = minTcpPingIntervalNextTimeLoopCount;
                                }

                                if (!tcpingRet)
                                {
                                    appendLog($@"[网络监测] 互联网故障
        tcpPingCheckFailCount       {tcpPingCheckFailCount}
        waitNextTimeLoopCounter     {waitNextTimeLoopCount}
        currentTcpPingInterval      {LoopCount2TimeSpan(currentTcpPingInterval)}
        minTcpPingInterval          {LoopCount2TimeSpan(minTcpPingIntervalNextTimeLoopCount)}
        maxTcpPingInterval          {LoopCount2TimeSpan(maxTcpPingIntervalNextTimeLoopCount)}");
                                }

                                if (restoredFromFailure)
                                {
                                    appendLog($@"[网络监测] 互联网恢复
        tcpPingCheckFailCount       {tcpPingCheckFailCount}
        waitNextTimeLoopCounter     {waitNextTimeLoopCount}
        currentTcpPingInterval      {LoopCount2TimeSpan(currentTcpPingInterval)}
        minTcpPingInterval          {LoopCount2TimeSpan(minTcpPingIntervalNextTimeLoopCount)}
        maxTcpPingInterval          {LoopCount2TimeSpan(maxTcpPingIntervalNextTimeLoopCount)}");
                                }

                                if (tcpPingCheckFailCount > Constant.MAX_TCP_PING_CHECK_ATTEMPT)
                                {
                                    showBalloonTip(Status.SHOW_AUTOMATIC_REDIAL_DUE_TO_NO_INTERNET, true);
                                    stopPPPoE();
                                    Constant.wait(Status.WAIT_RASDIAL); // PPPoE 重撥延遲 確保 IP 更換
                                    startPPPoE();
                                }
                            }

                            if (isBussinessWorkingHour())
                            {
                                // redial if connection duration is too long and during the working hour
                                var duration = getPPPoEActiveConnectionDuration();
                                if (duration != null && duration >=
                                    TimeSpan.FromMinutes(config.current.automaticRedialTimeoutMinutes))
                                {
                                    appendLog(
                                        $"current duration: {duration} is too long at {DateTime.Now}, prepare to reconnect");
                                    showBalloonTip(Status.SHOW_AUTOMATIC_REDIAL_DUE_TO_TIMEOUT, true);
                                    stopPPPoE();
                                    Constant.wait(Status.WAIT_RASDIAL); // PPPoE 重撥延遲 確保 IP 更換
                                    startPPPoE();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    appendLog("异常 : " + ex.Message);
                }

                Constant.wait(Status.WAIT_NEXT_TIME);
            }
        }

        private bool applyUIStart(bool automaticStart)
        {
            if (!config.canStart())
            {
                MessageBox.Show("拨号必须项非法，无法启动", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            WindowState   = FormWindowState.Minimized;
            ShowInTaskbar = false;

            showBalloonTip(automaticStart ? Status.SHOW_AUTOMATIC_START : Status.SHOW_START);
            return true;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (checkThread != null)
            {
                // stop clicked
                btnStart.Enabled = false;
                await Task.Run(() =>
                {
                    checkThread.Abort();
                    while (checkThread.ThreadState != ThreadState.Aborted)
                    {
                        Thread.Sleep(1);
                    }

                    checkThread = null;
                });
                appendLog("用户停止。");
                updateUIStatus();
                btnStart.Enabled = true;
            }
            else
            {
                // start clicked
                if (applyUIStart(false))
                {
                    if (Util.CanUseService() && config.current.automaticStartOnSystemBoot)
                    {
                        var winswStatus = CheckWinSWStatus();
                        if (winswStatus != WinSWStatus.NonExistent)
                        {
                            MessageBox.Show("已经安装自动运行系统服务，请通过系统服务管理运行状态\r\n本次操作无效", "錯誤", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }

                    startPPPoEThread(Status.MODE_WATCH_DOG);
                    updateUIStatus();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void updateAutomaticStartWaitUI()
        {
            numAutomaticStartWait.Enabled = chkAutomaticStart.Checked;
        }

        private void chkAutomaticStart_CheckedChanged(object sender, EventArgs e)
        {
            updateAutomaticStartWaitUI();
        }

        private void tsmiSetting_Click(object sender, EventArgs e)
        {
            showForm();
        }

        private void tsmiPause_Click(object sender, EventArgs e)
        {
            tsmiPause.Checked = !tsmiPause.Checked;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            exit();
        }

        private void exit()
        {
            niPermanent.Visible = false;
            Environment.Exit(Environment.ExitCode);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void tsmiIPTool_Click(object sender, EventArgs e)
        {
            Program.ipTool.Show();
        }

        private void niPermanent_DoubleClick(object sender, EventArgs e)
        {
            showForm();
        }

        private void showForm()
        {
            Show();
            ShowInTaskbar = true;
            WindowState   = FormWindowState.Normal;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? (char)0 : '*';
        }

        private void cbSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSetting.SelectedIndex == -1)
            {
                return;
            }

            config.select = cbSetting.SelectedItem.ToString();
            updateUIStatus();
        }

        private void btnManageSetting_Click(object sender, EventArgs e)
        {
            using (SettingManager manager = new SettingManager())
            {
                manager.ShowDialog();
            }

            updateUIStatus();
        }

        private void chkShowDebugLog_CheckedChanged(object sender, EventArgs e)
        {
            // 高度不变，只修改宽度
            Size originSize = new Size(466, this.Size.Height);
            Size extendSize = new Size(780, this.Size.Height);
            Size                  = chkShowDebugLog.Checked ? extendSize : originSize;
            labelDebugLog.Visible = txtDebugLog.Visible = chkShowDebugLog.Checked;
        }

        private void appendDesktopLog(string data)
        {
            string date = DateTime.Now.ToString("yyyy - MM - dd tt hh : mm : ss");
            Console.WriteLine($@"[{date}] {data}");
            txtDebugLog.BeginInvoke((Action)(() => txtDebugLog.AppendText($@"[{date}] {data}{Environment.NewLine}")));
        }

        public void appendLog(string data)
        {
            if (Program.IsRunAsService)
            {
                Util.appendServiceLog(data);
            }
            else
            {
                appendDesktopLog(data);
            }
        }

        private void chkAutomaticStartOnSystemBoot_CheckedChanged(object sender, EventArgs e)
        {
            // 使用服务必须开启自动拨号
            if (chkAutomaticStartOnSystemBoot.Checked)
            {
                chkAutomaticStart.Checked = true;
            }
        }
    }
}