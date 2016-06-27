using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace OneModel.Compression.Decompression
{
    public class SevenZipWrapper : ISevenZipWrapper
    {
        private readonly string _pathTo7Zip;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathTo7Zip">Path to the 7zip executable (include the actual filename).</param>
        public SevenZipWrapper(string pathTo7Zip)
        {
            _pathTo7Zip = pathTo7Zip;
        }
        
        public ITestResult Test(string inputPath)
        {
            var process = CreateProcess($"t \"{inputPath}\"");
            var result = new TestResult();
            
            RunProcess(process);

            var foundNotAnArchiveMessage = false;

            HandleOutput(process, line =>
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
                    result.PhysicalSize = int.Parse(line.Substring(16));
                }
                else if (line.StartsWith("Headers Size = "))
                {
                    result.HeadersSize = int.Parse(line.Substring(15));
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
                    result.Size = int.Parse(line.Substring(5).Trim());
                }
                else if (line.StartsWith("Compressed:"))
                {
                    result.CompressedSize = int.Parse(line.Substring(11).Trim());
                }
                else if (line == "Can't open as archive: 1")
                {
                    foundNotAnArchiveMessage = true;
                }
            });

            result.IsArchive = !foundNotAnArchiveMessage;

            result.ExitCode = process.ExitCode;

            process.Close();

            return result;
        }

        public IListResult List(string inputPath)
        {
            var process = CreateProcess($"l \"{inputPath}\"");
            var result = new ListResult();

            var pattern = "^(\\d{4}-\\d{2}-\\d{2} \\d{2}:\\d{2}:\\d{2}) (.{5})\\s+(\\d*)\\s+(\\d+)\\s+(.+)\\s*$";
            var regex = new Regex(pattern);
            var items = new List<ListResultItem>();
            result.Entries = items;

            RunProcess(process);
            
            HandleOutput(process, line =>
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
                    result.PhysicalSize = int.Parse(line.Substring(16));
                }
                else
                {
                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        var item = new ListResultItem
                        {
                            DateTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm:ss", null),
                            Attr = match.Groups[3].Value,
                            Size = int.Parse(match.Groups[4].Value),
                            Compressed = int.Parse(match.Groups[5].Value)
                        };
                        items.Add(item);
                    }
                }
            });

            result.ExitCode = process.ExitCode;

            process.Close();

            return result;
        }

        public IExtractResult Extract(string inputPath, string outputPath)
        {
            var process = CreateProcess($"e -o\"{outputPath}\" -y \"{inputPath}\"");
            var result = new ExtractResult();

            RunProcess(process);

            HandleOutput(process, line =>
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
                    result.PhysicalSize = int.Parse(line.Substring(16));
                }
                else if (line == "Everything is Ok")
                {
                    result.Ok = true;
                }
                else if (line.StartsWith("Size:"))
                {
                    result.Size = int.Parse(line.Substring(5).Trim());
                }
                else if (line.StartsWith("Compressed:"))
                {
                    result.CompressedSize = int.Parse(line.Substring(11).Trim());
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
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    FileName = _pathTo7Zip,
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