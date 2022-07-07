using System;
using System.Diagnostics;

namespace AutoPPPoE
{
    public static class CommandHelper
    {
        public static string runCommand(params string[] command)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName               = "cmd.exe";
                process.StartInfo.UseShellExecute        = false;
                process.StartInfo.RedirectStandardInput  = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError  = true;
                process.StartInfo.CreateNoWindow         = true;

                string stdOut;
                try
                {
                    process.Start();
                    foreach (var singleCmd in command)
                    {
                        process.StandardInput.WriteLine(singleCmd);
                    }

                    process.StandardInput.WriteLine("exit");

                    stdOut = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    stdOut = ex.Message;
                }

                return stdOut;
            }
        }

        public static string runCommandNoStdIn(string exe, string args = null)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = exe;
                if (!string.IsNullOrEmpty(args) && !string.IsNullOrWhiteSpace(args))
                {
                    process.StartInfo.Arguments = args;
                }

                process.StartInfo.UseShellExecute = false;
                //process.StartInfo.RedirectStandardInput  = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError  = true;
                process.StartInfo.CreateNoWindow         = true;

                string stdOut;
                try
                {
                    process.Start();

                    stdOut =  process.StandardOutput.ReadToEnd();
                    stdOut += process.StandardError.ReadToEnd();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    stdOut = ex.Message;
                }

                return stdOut;
            }
        }
    }
}