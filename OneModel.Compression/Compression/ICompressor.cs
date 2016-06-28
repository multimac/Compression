namespace OneModel.Compression.Compression
{
    public interface ICompressor
    {
        ICompressResult Compress(string inputPath, string outputPath);
    }
}
