using System.Diagnostics;
using System.IO;

namespace OneModel.Compression.Processes
{
    public interface IProcessWrapper
    {
        int ExitCode { get; }
        bool HasExited { get; }
        ProcessStartInfo StartInfo { get; set; }
        StreamReader StandardOutput { get; }

        void Close();
        bool Start();
        bool WaitForExit(int milliseconds);
        void WaitForExit();
    }
}