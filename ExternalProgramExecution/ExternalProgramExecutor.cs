using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ExternalProgramExecution
{
    public abstract class ExternalProgramExecutor : IExternalProgramExecutor
    {
        public abstract string ProgramPath { get; }
        public abstract int MaxWaitTime { get; }
        public abstract string Arguments { get; }

        public void Execute(bool showOutput)
        {
            var success = false;
            var timedOut = false;
            var programPath = ProgramPath;
            var arguments = Arguments;
            var maxWaitTime = MaxWaitTime;

            using (Process build = new Process())
            {
                build.StartInfo.FileName = programPath;
                build.StartInfo.Arguments = arguments;
                build.StartInfo.UseShellExecute = false;
                if (!showOutput)
                {
                    build.StartInfo.RedirectStandardOutput = true;
                    build.StartInfo.RedirectStandardError = true;
                    build.StartInfo.CreateNoWindow = true;
                    build.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }


                build.Start();

                if (build.WaitForExit(maxWaitTime))
                {
                    success = build.ExitCode == 0;
                }
                else
                {
                    timedOut = true;
                }
            }
            if (timedOut)
            {
                OnTimeOut(programPath, arguments, maxWaitTime);
            }
            if (!success)
            {
                OnUnsuccessful(programPath, arguments);
            }
        }

        public async Task<int> ExecuteAsync(bool showOutput)
        {
            var programPath = ProgramPath;
            var arguments = Arguments;

            var taskCompletionSource = new TaskCompletionSource<int>();

            Process build = new Process();

            build.StartInfo.FileName = programPath;
            build.StartInfo.Arguments = arguments;
            build.StartInfo.UseShellExecute = false;
            build.EnableRaisingEvents = true;

            if (!showOutput)
            {
                build.StartInfo.RedirectStandardOutput = true;
                build.StartInfo.RedirectStandardError = true;
                build.StartInfo.CreateNoWindow = true;
                build.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }

            /*build.Exited += (sender, args) =>
            {
                var exitCode = build.ExitCode;
                taskCompletionSource.SetResult(exitCode);
                if (exitCode != 0)
                {
                    OnUnsuccessful(programPath, arguments);
                }
                build.Dispose();
            };*/

            build.Start();

            var waitForExit = WaitForExitAsync(build, MaxWaitTime);
            var processTask = Task.WhenAll(waitForExit);
            await processTask;

            if (build.HasExited)
            {
                var exitCode = build.ExitCode;
                build.Dispose();
                if (exitCode != 0)
                {
                    OnUnsuccessful(programPath, arguments);
                }
                return exitCode;
            }
            else
            {
                try
                {
                    build.CloseMainWindow();
                }
                catch
                {
                    try
                    {
                        build.Kill();
                    }
                    catch
                    {
                        //build already dead
                    }
                }
                finally
                {
                    build.Dispose();
                }
                OnTimeOut(programPath, arguments, MaxWaitTime);
                return -1;
            }
        }

        public virtual void OnTimeOut(string program, string arguments, int timeout)
        {
            throw new ProcessException($"Program: {program} with arguments: {arguments} took over {timeout} miliseconds and was timed out");
        }
        public virtual void OnUnsuccessful(string program, string arguments)
        {
            throw new ProcessException($"Program: {program} with arguments: {arguments} was unsuccessful");
        }
        private Task<bool> WaitForExitAsync(Process process, int timeout)
        {
            return Task.Run(() => process.WaitForExit(timeout));
        }
    }
}
