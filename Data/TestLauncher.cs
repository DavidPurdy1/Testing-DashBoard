using System;
using System.Diagnostics;

namespace BlazorApp.Data
{
    public class TestLauncher
    {
        public bool InProgress = true;
        public void Runner(string tests)
        {
            InProgress = true;
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            cmdStartInfo.RedirectStandardOutput = true;
            cmdStartInfo.RedirectStandardError = true;
            cmdStartInfo.RedirectStandardInput = true;
            cmdStartInfo.UseShellExecute = false;
            //Change to false to see what is going on or open debug console. 
            cmdStartInfo.CreateNoWindow = true;

            Process cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.ErrorDataReceived += cmd_Error;
            cmdProcess.OutputDataReceived += cmd_Output;
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.BeginErrorReadLine();
            //Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\vsdevcmd.bat
            //Sets it to a Dev Prompt to be able to run mstest or vstest
            cmdProcess.StandardInput.WriteLine("cd " + @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\");
            cmdProcess.StandardInput.WriteLine("vsdevcmd.bat");
            cmdProcess.StandardInput.WriteLine("cd " + @"C:\Users\i00018\source\repos\Console Tests\bin\Debug");
            cmdProcess.StandardInput.WriteLine("vstest.console.exe " + '"' + "Console Tests.dll" + '"' + " /Tests:" + tests);
            cmdProcess.StandardInput.WriteLine("exit");
            //cmdProcess.WaitForExit();
            InProgress = false;
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