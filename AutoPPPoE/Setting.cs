using System;
using Newtonsoft.Json;

namespace AutoPPPoE
{
    [Serializable]
    public class Setting
    {
        private const int ALLOW_MAX_SLOW_PING_WAIT = 3600 * 1000;
        private const int ALLOW_MAX_AUTOMATIC_START_WAIT_TIME = 3600;

        public string adapter { get; private set; }

        public string name { get; private set; }

        public string account { get; set; }


        public string password { get; set; }

        [JsonIgnore]
        public string plainTextPassword
        {
            get => Util.DecryptPassword(this.password);
        }

        public int tcpPing { get; private set; }
        public string tcpPingHost { get; private set; }
        public int tcpPingPort { get; private set; }

        public bool automaticStart { get; private set; }

        public bool automaticStartOnSystemBoot { get; private set; }

        public int automaticStartWaitTime { get; private set; }

        public int automaticRedialTimeoutMinutes { get; private set; }

        public Setting(string adapter, string name, string account, string password, int tcpPing,
            string tcpPingHost, int tcpPingPort,
            bool automaticStart, bool automaticStartOnSystemBoot,
            int automaticStartWaitTime, int automaticRedialTimeoutMinutes)
        {
            AdapterManager adapterManager = Program.adapter;
            if (!adapterManager.adapterName.Contains(adapter))
            {
                throw new EWException("网卡名称不存在");
            }

            if (!adapterManager.rasName.Contains(name))
            {
                throw new EWException("PPPoE 适配器不存在");
            }

            if (tcpPing < 1 || tcpPing > ALLOW_MAX_SLOW_PING_WAIT)
            {
                throw new EWException("TCP Ping 等待时间参数无效");
            }

            if (string.IsNullOrEmpty(tcpPingHost) || string.IsNullOrWhiteSpace(tcpPingHost))
            {
                throw new EWException("TCP Ping Host参数无效");
            }

            if (tcpPingPort <= 0 || tcpPingPort > 65535)
            {
                throw new EWException("TCP Ping Port参数无效");
            }

            if (automaticStartWaitTime < 0 || automaticStartWaitTime > ALLOW_MAX_AUTOMATIC_START_WAIT_TIME)
            {
                throw new EWException("启动等待时间参数无效");
            }

            if (automaticRedialTimeoutMinutes < 0)
            {
                throw new EWException("自动重拨分钟数参数无效");
            }

            this.adapter                       = adapter;
            this.name                          = name;
            this.account                       = account;
            this.password                      = password;
            this.tcpPing                       = tcpPing;
            this.tcpPingHost                   = tcpPingHost;
            this.tcpPingPort                   = tcpPingPort;
            this.automaticStart                = automaticStart;
            this.automaticStartOnSystemBoot    = automaticStartOnSystemBoot;
            this.automaticStartWaitTime        = automaticStartWaitTime;
            this.automaticRedialTimeoutMinutes = automaticRedialTimeoutMinutes;
        }
    }
}