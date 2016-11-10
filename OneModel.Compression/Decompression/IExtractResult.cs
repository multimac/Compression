namespace OneModel.Compression.Decompression
{
    public interface IExtractResult
    {
        int ExitCode { get; }

        string Path { get; }

        string Type { get; }

        long PhysicalSize { get; }

        bool Ok { get; }

        long Size { get; }

        long CompressedSize { get; }
    }
}