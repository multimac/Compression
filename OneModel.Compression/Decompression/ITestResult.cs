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

        int PhysicalSize { get; }

        int HeadersSize { get; }

        string CodePage { get; }

        bool Ok { get; }

        int Size { get; }

        int CompressedSize { get; }
    }
}