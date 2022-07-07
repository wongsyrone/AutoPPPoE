using System;
using System.Net.Sockets;

namespace AutoPPPoE
{
    public static class PingHelper
    {
        public static bool pingHost(string host, int port, int timeout)
        {
            bool   pingable = false;
            Socket s        = null;
            try
            {
                s = new Socket(SocketType.Stream, ProtocolType.Tcp) { Blocking = false };

                var result = s.BeginConnect(host, port,
                    null,
                    null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeout), true);
                if (success)
                {
                    s.EndConnect(result);
                    pingable = true;
                }

                return pingable;
            }
            catch (SocketException)
            {
                return false;
            }
            finally
            {
                s?.Close();
            }
        }
    }
}