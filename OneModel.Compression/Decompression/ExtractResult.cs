namespace OneModel.Compression.Decompression
{
    public class ExtractResult : IExtractResult
    {
        public int ExitCode { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public int PhysicalSize { get; set; }

        public bool Ok { get; set; }

        public int Size { get; set; }

        public int CompressedSize { get; set; }
    }
}