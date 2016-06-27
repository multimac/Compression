namespace OneModel.Compression.Compression
{
    public interface ICompressResult
    {
        int ExitCode { get; }
        bool Ok { get; }
    }
}