using System;
using System.IO;
using System.Threading;

namespace AutoPPPoE
{
    public enum Status
    {
        WAIT_NEXT_TIME,
        WAIT_RASDIAL,
        WAIT_ADAPTER,
        SHOW_WELCOME,
        SHOW_START,
        SHOW_AUTOMATIC_START,
        SHOW_ADAPTER,
        SHOW_CONNECT,
        SHOW_DISCONNECT,
        SHOW_AUTOMATIC_REDIAL_DUE_TO_NO_INTERNET,
        SHOW_AUTOMATIC_REDIAL_DUE_TO_TIMEOUT,
        SHOW_NO_AUTOMATIC_STARTUP_SERVICE_WINSW,
        MODE_START_UP,
        MODE_WATCH_DOG
    }

    public static class Constant
    {
        public const string winswServiceExeName = "autopppoe-service.exe";
        public static string winswServiceExePath = Path.Combine(Util.WorkingDirectory, "service", winswServiceExeName);
        public const string DEFAULT_TCP_PING_CHECK_HOST = "www.baidu.com";
        public const int DEFAULT_TCP_PING_CHECK_PORT = 443;
        public const int DEFAULT_TCP_PING_WAIT_TIME = 5 * 1000;
        public const bool DEFAULT_AUTOMATIC_START = false;
        public const bool DEFAULT_AUTOMATIC_START_ON_SYSTEM_BOOT = false;
        public const int DEFAULT_AUTOMATIC_START_WAIT_TIME = 5;
        public const int DEFAULT_AUTOMATIC_REDIAL_TIMEOUT_MINUTES = 9 * 60;

        public const byte MAX_FETCH_ATTEMPT = 3;

        public const int MAX_TCP_PING_CHECK_ATTEMPT = 4;

        public const int WAIT_NEXT_TIME_DELAY = 3000;
        public const int WAIT_NETWORK_STATUS_CHANGE_DELAY = 5000;
        public const int TOOL_TIP_SHOW_DURATION = 5000;

        public const string IP_FETCH_URL = "https://ip4.seeip.org/";

        public const string AES_KEY = "ew.sr.x1c.quilt.meow";

        public static void wait(Status mode)
        {
            switch (mode)
            {
                case Status.WAIT_NEXT_TIME:
                {
                    Thread.Sleep(WAIT_NEXT_TIME_DELAY);
                    break;
                }
                case Status.WAIT_RASDIAL:
                case Status.WAIT_ADAPTER:
                {
                    Thread.Sleep(WAIT_NETWORK_STATUS_CHANGE_DELAY);
                    break;
                }
                default:
                {
                    throw new ArgumentException("参数错误");
                }
            }
        }
    }
}