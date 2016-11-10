namespace OneModel.Compression.Decompression
{
    /// <summary>
    /// The result of testing the integrity of an archive.
    /// </summary>
    public interface ITestResult
    {
        int ExitCode { get; }

        bool IsArchive { get; }

        string Path { get; }

        string Type { get; }

        long PhysicalSize { get; }

        long HeadersSize { get; }

        string CodePage { get; }

        bool Ok { get; }

        long Size { get; }

        long CompressedSize { get; }
    }
}