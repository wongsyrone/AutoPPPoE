using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoPPPoE
{
    public static class Util
    {
        public static readonly string ExecutablePath = Process.GetCurrentProcess().MainModule?.FileName ?? "";
        public static readonly string WorkingDirectory = Path.GetDirectoryName(ExecutablePath) ?? "";

        public static bool CanUseService()
        {
            return File.Exists(Constant.winswServiceExePath);
        }

        public static void forceUpdateNumericUpDownValue(NumericUpDown control, decimal value)
        {
            decimal pre = value + 1;
            if (pre > control.Maximum)
            {
                pre = value - 1;
            }

            control.Value = pre;
            control.Value = value;
        }

        public static void optionSelect(ComboBox cb, string option)
        {
            if (cb.Items.Count <= 0)
            {
                throw new EWException("選單不能為空");
            }

            for (int i = 0; i < cb.Items.Count; ++i)
            {
                if (cb.Items[i].ToString() == option)
                {
                    cb.SelectedIndex = i;
                    return;
                }
            }

            throw new EWException("找不到指定項目 : " + option);
        }

        public static void loadSettingNameUI(ComboBox cb)
        {
            cb.Items.Clear();
            Config config = Program.config;
            foreach (KeyValuePair<string, Setting> data in config.setting)
            {
                cb.Items.Add(data.Key);
            }

            if (cb.Items.Count > 0)
            {
                optionSelect(cb, config.select);
            }
        }

        public static Setting generateDefaultSetting()
        {
            AdapterManager adapter = Program.adapter;
            return new Setting(adapter.adapterName[0],
                adapter.rasName[0],
                string.Empty,
                Util.EncryptPassword(string.Empty),
                Constant.DEFAULT_TCP_PING_WAIT_TIME,
                Constant.DEFAULT_TCP_PING_CHECK_HOST,
                Constant.DEFAULT_TCP_PING_CHECK_PORT,
                Constant.DEFAULT_AUTOMATIC_START,
                Constant.DEFAULT_AUTOMATIC_START_ON_SYSTEM_BOOT,
                Constant.DEFAULT_AUTOMATIC_START_WAIT_TIME,
                Constant.DEFAULT_AUTOMATIC_REDIAL_TIMEOUT_MINUTES);
        }

        public static bool isValidModifySettingName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !Program.config.setting.ContainsKey(name);
        }

        public static string EncryptPassword(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }

            return SimpleAES.AESEncryptBase64(plainText, Constant.AES_KEY);
        }

        public static string DecryptPassword(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }

            return SimpleAES.AESDecryptBase64(cipherText, Constant.AES_KEY);
        }

        public static string removeWhiteSpace(string data)
        {
            return Regex.Replace(data, "\\s+", string.Empty);
        }

        public static bool isValidIP4Address(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return false;
            }

            string[] split = data.Split('.');
            if (split.Length != 4)
            {
                return false;
            }

            byte parse;
            return split.All(value => byte.TryParse(value, out parse));
        }

        public static string GetNicConnId(string combinedName)
        {
            var idx = combinedName.LastIndexOf("$", StringComparison.InvariantCulture);
            return combinedName.Substring(idx + 1);
        }

        public static string GetNicName(string combinedName)
        {
            var idx = combinedName.LastIndexOf("$", StringComparison.InvariantCulture);
            return combinedName.Substring(0, idx);
        }

        public static void appendServiceLog(string msg)
        {
            string date = DateTime.Now.ToString("yyyy - MM - dd tt hh : mm : ss");
            Console.WriteLine($@"[{date}] [SERVICE] {msg}");
        }
    }
}