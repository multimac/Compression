using System.Diagnostics;
using System.IO;

namespace OneModel.Compression.Processes
{
    public class ProcessWrapper : IProcessWrapper
    {
        private readonly Process _process;

        public ProcessWrapper()
        {
            _process = new Process();
        }

        public int ExitCode => _process.ExitCode;

        public bool HasExited => _process.HasExited;

        public StreamReader StandardOutput => _process.StandardOutput;

        public ProcessStartInfo StartInfo
        {
            get { return _process.StartInfo; }
            set { _process.StartInfo = value; }
        }

        public void Close()
        {
            _process.Close();
        }

        public bool Start()
        {
            return _process.Start();
        }

        public bool WaitForExit(int milliseconds)
        {
            return _process.WaitForExit(milliseconds);
        }

        public void WaitForExit()
        {
            _process.WaitForExit();
        }
    }
}
