using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using OneModel.Compression.Processes;

namespace OneModel.Compression.Decompression
{
    public class SevenZipWrapper : ISevenZipWrapper
    {
        private readonly IProcessWrapper _process;
        private readonly string _pathTo7Zip;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathTo7Zip">Path to the 7zip executable (include the actual filename).</param>
        /// <param name="process">Process implementation to use. Defaults to <see cref="T:OneModel.Compression.Processes.ProcessWrapper" />.</param>
        public SevenZipWrapper(string pathTo7Zip, IProcessWrapper process = null)
        {
            _pathTo7Zip = pathTo7Zip;
            _process = process ?? new ProcessWrapper();
        }
        
        public ITestResult Test(string inputPath)
        {
            SetupProcess(_process, $"t \"{inputPath}\"");
            var result = new TestResult();
            
            RunProcess(_process);

            var foundNotAnArchiveMessage = false;

            HandleOutput(_process, line =>
            {
                if (line == null)
                {

                }
                else if (line.StartsWith("Type = "))
                {
                    result.Type = line.Substring(7);
                }
                else if (line.StartsWith("Path = "))
                {
                    result.Path = line.Substring(7);
                }
                else if (line.StartsWith("Physical Size = "))
                {
                    result.PhysicalSize = long.Parse(line.Substring(16));
                }
                else if (line.StartsWith("Headers Size = "))
                {
                    result.HeadersSize = long.Parse(line.Substring(15));
                }
                else if (line.StartsWith("Code Page = "))
                {
                    result.CodePage = line.Substring(12);
                }
                else if (line == "Everything is Ok")
                {
                    result.Ok = true;
                }
                else if (line.StartsWith("Size:"))
                {
                    result.Size = long.Parse(line.Substring(5).Trim());
                }
                else if (line.StartsWith("Compressed:"))
                {
                    result.CompressedSize = long.Parse(line.Substring(11).Trim());
                }
                else if (line == "Can't open as archive: 1")
                {
                    foundNotAnArchiveMessage = true;
                }
            });

            result.IsArchive = !foundNotAnArchiveMessage;

            result.ExitCode = _process.ExitCode;

            _process.Close();

            return result;
        }

        public IListResult List(string inputPath)
        {
            SetupProcess(_process, $"l \"{inputPath}\"");
            var result = new ListResult();

            var pattern = "^(\\d{4}-\\d{2}-\\d{2} \\d{2}:\\d{2}:\\d{2}) (.{5})\\s+(\\d*)\\s+(\\d+)\\s+(.+)\\s*$";
            var regex = new Regex(pattern);
            var items = new List<ListResultItem>();
            result.Entries = items;

            RunProcess(_process);
            
            HandleOutput(_process, line =>
            {
                if (line == null)
                {
                    return;
                }

                if (line.StartsWith("Type = "))
                {
                    result.Type = line.Substring(7);
                }
                else if (line.StartsWith("Path = "))
                {
                    result.Path = line.Substring(7);
                }
                else if (line.StartsWith("Physical Size = "))
                {
                    result.PhysicalSize = long.Parse(line.Substring(16));
                }
                else
                {
                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        var item = new ListResultItem
                        {
                            DateTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm:ss", null),
                            Attr = match.Groups[2].Value,
                            Size = long.Parse(match.Groups[3].Value),
                            Compressed = long.Parse(match.Groups[4].Value)
                        };
                        items.Add(item);
                    }
                }
            });

            result.ExitCode = _process.ExitCode;

            _process.Close();

            return result;
        }

        public IExtractResult Extract(string inputPath, string outputPath)
        {
            SetupProcess(_process, $"e -o\"{outputPath}\" -y \"{inputPath}\"");
            var result = new ExtractResult();

            RunProcess(_process);

            HandleOutput(_process, line =>
            {
                if (line == null)
                {
                    return;
                }

                if (line.StartsWith("Type = "))
                {
                    result.Type = line.Substring(7);
                }
                else if (line.StartsWith("Path = "))
                {
                    result.Path = line.Substring(7);
                }
                else if (line.StartsWith("Physical Size = "))
                {
                    result.PhysicalSize = long.Parse(line.Substring(16));
                }
                else if (line == "Everything is Ok")
                {
                    result.Ok = true;
                }
                else if (line.StartsWith("Size:"))
                {
                    result.Size = long.Parse(line.Substring(5).Trim());
                }
                else if (line.StartsWith("Compressed:"))
                {
                    result.CompressedSize = long.Parse(line.Substring(11).Trim());
                }
            });

            result.ExitCode = _process.ExitCode;

            _process.Close();

            return result;
        }
        
        /// <summary>
        /// Assigns new StartInfo using the 7zip exe, with the given
        /// arguments, to the provided process.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private void SetupProcess(IProcessWrapper process, string args)
        {
            process.StartInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                FileName = _pathTo7Zip,
                Arguments = args
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