using System;
using System.Diagnostics;
using System.Configuration;
using System.Threading.Tasks;
namespace BlazorApp.Services
{
    public class TestLauncher
    {
        public static async Task<int> RunProcessAsync(string tests)
        {
            using (var process = new Process
            {
                StartInfo ={
                    FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\Powershell.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                },
                EnableRaisingEvents = true
            })
            {
                return await RunProcessAsync(process, tests).ConfigureAwait(false);
            }
        }
        private static Task<int> RunProcessAsync(Process process, string tests)
        {
            var tcs = new TaskCompletionSource<int>();

            process.Exited += (s, ea) => tcs.SetResult(process.ExitCode);
            process.OutputDataReceived += (s, ea) => Console.WriteLine(ea.Data);
            process.ErrorDataReceived += (s, ea) => Console.WriteLine("ERR: " + ea.Data);

            if (!process.Start())
            {
                throw new InvalidOperationException("Could not start process: " + process);
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.StandardInput.WriteLine("cd " + '"' + ConfigurationManager.AppSettings.Get("Vstest.exe") + '"');
            process.StandardInput.Write(@".\" + "vstest.console.exe ");
            process.StandardInput.WriteLine('"' + ConfigurationManager.AppSettings.Get("TestLocation") + '"' + " /Tests:" + tests);
            process.StandardInput.WriteLine("exit");

            Console.WriteLine("It has finished running the process");
            return tcs.Task;
        }
    }
}