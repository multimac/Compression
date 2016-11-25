using System;
using System.Diagnostics;
using OneModel.Compression.Processes;

namespace OneModel.Compression.Compression
{
    public class LzoCompressor : ICompressor
    {
        private readonly string _pathToLzop;

        public LzoCompressor(string pathToLzop)
        {
            _pathToLzop = pathToLzop;
        }

        public ICompressResult Compress(string inputPath, string outputPath)
        {
            var process = CreateProcess($"-o \"{outputPath}\" \"{inputPath}\"");

            RunProcess(process);

            var result = new CompressResult();

            process.WaitForExit();

            result.ExitCode = process.ExitCode;

            if (result.ExitCode == 0)
                result.Ok = true;

            process.Close();

            return result;
        }

        /// <summary>
        /// Creates a new process using the 7zip exe, with the given
        /// arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private IProcessWrapper CreateProcess(string args)
        {
            return new ProcessWrapper
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    FileName = _pathToLzop,
                    Arguments = args
                }
            };
        }

        /// <summary>
        /// Runs a process, and waits for it to end.
        /// </summary>
        /// <param name="process"></param>
        private void RunProcess(IProcessWrapper process)
        {
            process.Start();

            while (!process.HasExited)
            {
                process.WaitForExit(1000);
            }
        }

        private void HandleOutput(IProcessWrapper process, Action<string> handler)
        {
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                handler(line);
            }
        }
    }
}