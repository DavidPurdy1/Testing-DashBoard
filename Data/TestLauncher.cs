using System;
using System.Diagnostics;
using System.Configuration;
namespace BlazorApp.Data
{
    public class TestLauncher
    {
        public void Runner(string tests)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\Powershell.exe";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process shellProcess = new Process();
            shellProcess.StartInfo = startInfo;
            shellProcess.ErrorDataReceived += cmd_Error;
            shellProcess.OutputDataReceived += cmd_Output;
            shellProcess.EnableRaisingEvents = true;
            shellProcess.Start();
            shellProcess.BeginOutputReadLine();
            shellProcess.BeginErrorReadLine();
            shellProcess.StandardInput.WriteLine("cd " + '"' + ConfigurationManager.AppSettings.Get("Vstest.exe") + '"');
            shellProcess.StandardInput.Write(@".\" + "vstest.console.exe ");
            shellProcess.StandardInput.WriteLine('"' + ConfigurationManager.AppSettings.Get("TestLocation") + '"' + " /Tests:" + tests);
            shellProcess.StandardInput.WriteLine("exit");
        }

        public void cmd_Output(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        public void cmd_Error(object sender, DataReceivedEventArgs e)
        {
            Console.Write("Error >> ");
            Console.WriteLine(e.Data);
        }
    }
}