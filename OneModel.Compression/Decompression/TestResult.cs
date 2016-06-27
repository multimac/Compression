namespace OneModel.Compression.Decompression
{
    public class TestResult : ITestResult
    {
        public int ExitCode { get; set; }

        public bool IsArchive { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public int PhysicalSize { get; set; }

        public int HeadersSize { get; set; }

        public string CodePage { get; set; }

        public bool Ok { get; set; }

        public int Size { get; set; }

        public int CompressedSize { get; set; }

    }
}