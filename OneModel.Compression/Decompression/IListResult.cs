using System.Collections.Generic;

namespace OneModel.Compression.Decompression
{
    public interface IListResult
    {
        int ExitCode { get; }

        string Path { get;  }

        string Type { get; }

        long PhysicalSize { get; }

        IReadOnlyCollection<IListResultItem> Entries { get; } 
    }
}