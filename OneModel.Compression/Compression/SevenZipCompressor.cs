using System;
using System.Diagnostics;

namespace OneModel.Compression.Compression
{
    public class SevenZipCompressor : ICompressor
    {
        private readonly string _pathToSevenZip;

        public SevenZipCompressor(string pathToSevenZip)
        {
            _pathToSevenZip = pathToSevenZip;
        }

        public ICompressResult Compress(string inputPath, string outputPath)
        {
            var process = CreateProcess($"a -tzip \"{outputPath}\" \"{inputPath}\"");

            RunProcess(process);

            var result = new CompressResult();

            HandleOutput(process, line =>
            {
                if (line != null && line.EndsWith(" OK"))
                {
                    result.Ok = true;
                }
            });

            result.ExitCode = process.ExitCode;

            process.Close();

            return result;
        }

        /// <summary>
        /// Creates a new process using the 7zip exe, with the given
        /// arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private Process CreateProcess(string args)
        {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    FileName = _pathToSevenZip,
                    Arguments = args
                }
            };
        }

        /// <summary>
        /// Runs a process, and waits for it to end.
        /// </summary>
        /// <param name="process"></param>
        private void RunProcess(Process process)
        {
            process.Start();

            while (!process.HasExited)
            {
                process.WaitForExit(1000);
            }
        }

        private void HandleOutput(Process process, Action<string> handler)
        {
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                handler(line);
            }
        }
    }
}
