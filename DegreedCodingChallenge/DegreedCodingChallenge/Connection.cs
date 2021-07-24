using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebApplication
{
    public static class Connection
    {
        private static int Port { get; } = 8080;
        private static Uri DevServerEndpoint { get; } = new Uri($"http://localhost:{Port}");

        public static void UserVueDevServer(this ISpaBuilder spa)
        {
            spa.UseProxyToSpaDevelopmentServer(async () =>
            {
                if (IPGlobalProperties.GetIPGlobalProperties()
                        .GetActiveTcpListeners()
                        .Select(x => x.Port)
                        .Contains(Port))
                {
                    return DevServerEndpoint;
                }

                var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                var processInfo = new ProcessStartInfo
                {
                    FileName = isWindows ? "cmd" : "npm",
                    Arguments = $"{(isWindows ? "/c npm " : "")}run server",
                    WorkingDirectory = "joke-ui",
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                var process = Process.Start(processInfo);
                var tcs = new TaskCompletionSource<int>();

                _ = Task.Run(() =>
                {
                    try
                    {
                        string line;
                        while ((line = process.StandardOutput.ReadLine()) != null)
                        {
                            if (!tcs.Task.IsCompleted && line.Contains("DONE  Compiled successfully in"))
                            {
                                tcs.SetResult(1);
                            }
                        }
                    }
                    catch (EndOfStreamException ex)
                    {
                        tcs.SetException(new InvalidOperationException("'npm run serve' failed.", ex));
                    }
                });

                var timeout = Task.Delay(TimeSpan.FromSeconds(60));
                if (await Task.WhenAny(timeout, tcs.Task) == timeout)
                {
                    throw new TimeoutException();
                }

                return DevServerEndpoint;
            });
        }
    }

}