namespace OneModel.Compression.Decompression
{
    public interface IExtractResult
    {
        int ExitCode { get; }

        string Path { get; }

        string Type { get; }

        int PhysicalSize { get; }

        bool Ok { get; }

        int Size { get; }

        int CompressedSize { get; }
    }
}