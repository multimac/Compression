namespace OneModel.Compression.Compression
{
    public interface ILzoCompressor
    {
        ICompressResult Compress(string inputPath, string outputPath);
    }
}
