namespace OneModel.Compression.Decompression
{
    public class TestResult : ITestResult
    {
        public int ExitCode { get; set; }

        public bool IsArchive { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public long PhysicalSize { get; set; }

        public long HeadersSize { get; set; }

        public string CodePage { get; set; }

        public bool Ok { get; set; }

        public long Size { get; set; }

        public long CompressedSize { get; set; }

    }
}