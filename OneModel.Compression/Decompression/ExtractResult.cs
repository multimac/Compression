namespace OneModel.Compression.Decompression
{
    public class ExtractResult : IExtractResult
    {
        public int ExitCode { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public long PhysicalSize { get; set; }

        public bool Ok { get; set; }

        public long Size { get; set; }

        public long CompressedSize { get; set; }
    }
}