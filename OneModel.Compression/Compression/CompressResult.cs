namespace OneModel.Compression.Compression
{
    public class CompressResult : ICompressResult
    {
        public int ExitCode { get; set; }

        public bool Ok { get; set; }
    }
}