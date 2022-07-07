using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AutoPPPoE
{
    [Serializable]
    public class Config
    {
        private const string CONFIG_PATH = "config.json";
        private string _select;

        public IDictionary<string, Setting> setting { get; private set; }

        public string select
        {
            get { return _select; }
            set
            {
                checkSelect(setting, value);
                _select = value;
            }
        }

        private static void checkSelect(IDictionary<string, Setting> setting, string select)
        {
            if (select == null)
            {
                if (setting.Count > 0)
                {
                    throw new EWException("设定名称无法为空");
                }
            }
            else
            {
                if (!setting.ContainsKey(select))
                {
                    throw new EWException("无效的设定选项");
                }
            }
        }

        [JsonIgnore]
        public Setting current
        {
            get => setting[_select];
            set
            {
                if (!setting.ContainsKey(_select))
                {
                    throw new EWException("无效的设定名称");
                }

                setting[_select] = value ?? throw new EWException("无效的设定值");
            }
        }

        public Config()
        {
            setting = new Dictionary<string, Setting>();
        }

        public void loadSetting()
        {
            try
            {
                if (!File.Exists(CONFIG_PATH))
                {
                    return;
                }

                string fileContent;
                using (FileStream fs = new FileStream(CONFIG_PATH, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader br = new StreamReader(fs))
                    {
                        fileContent = br.ReadToEnd();
                    }
                }

                Config readSetting = JsonConvert.DeserializeObject<Config>(fileContent);

                if (readSetting is null)
                {
                    throw new EWException("解析config错误，参照现有文件重新手动配置");
                }

                checkSelect(readSetting.setting, readSetting._select);

                setting = readSetting.setting;
                _select = readSetting._select;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取设定档发生异常： " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void saveSetting()
        {
            try
            {
                using (FileStream fs = new FileStream(CONFIG_PATH, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter bw = new StreamWriter(fs))
                    {
                        bw.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
                        bw.Flush();
                    }
                }

                MessageBox.Show("储存设定成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("储存设定档发生异常 : " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool canStart()
        {
            return !string.IsNullOrWhiteSpace(current.account) && !string.IsNullOrWhiteSpace(current.password);
        }
    }
}