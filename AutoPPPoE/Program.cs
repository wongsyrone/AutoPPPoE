using SkinSharp;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AutoPPPoE
{
    public static class Program
    {
        private const int EXIT_FAILURE = 1;

        private static SkinH_Net skin;

        public static IPTool ipTool { get; private set; }

        public static AdapterManager adapter { get; private set; }

        public static Config config { get; private set; }

        public static MainForm main { get; private set; }

        public static bool IsRunAsService = !Environment.UserInteractive || Process.GetCurrentProcess().SessionId == 0;

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            using (Mutex mutex = new Mutex(false, $@"Global\AutoPPPoE_"))
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                // handle UI exceptions
                Application.ThreadException += Application_ThreadException;
                // handle non-UI exceptions
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    skin = new SkinH_Net();
                    skin.AttachEx("Skin.she", "");
                }
                catch
                {
                    Application.EnableVisualStyles();
                }

                if (!mutex.WaitOne(0, false))
                {
                    string name = Process.GetCurrentProcess().ProcessName;
                    if (Process.GetProcessesByName(name).Length > 1)
                    {
                        var msg = "已经有其他实例正在运行";

                        if (IsRunAsService)
                        {
                            Util.appendServiceLog(msg);
                        }
                        else
                        {
                            MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        return;
                    }
                }


                adapter = new AdapterManager();
                if (adapter.adapterName.Count <= 0 || adapter.rasName.Count <= 0)
                {
                    var msg = "找不到网卡或者拨号链接配置";
                    if (IsRunAsService)
                    {
                        Util.appendServiceLog(msg);
                    }
                    else
                    {
                        MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Environment.Exit(EXIT_FAILURE);
                }

                ipTool = new IPTool();

                config = new Config();
                config.loadSetting();
                main = new MainForm();
                main.InitLoad();
                Application.Run(main);
            }
        }

        private static int exited = 0;

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (Interlocked.Increment(ref exited) == 1)
            {
                string errMsg = e.ExceptionObject.ToString();

                if (IsRunAsService)
                {
                    Util.appendServiceLog($"non-UI error: {errMsg}");
                }
                else
                {
                    MessageBox.Show(errMsg, "non-UI 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Application.Exit();
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (Interlocked.Increment(ref exited) == 1)
            {
                string errorMsg = $"Exception Detail: {Environment.NewLine}{e.Exception}";
                if (IsRunAsService)
                {
                    Util.appendServiceLog($"non-UI error: {errorMsg}");
                }
                else
                {
                    MessageBox.Show(errorMsg, "UI 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Application.Exit();
            }
        }
    }
}