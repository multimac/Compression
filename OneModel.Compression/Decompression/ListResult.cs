using System.Collections.Generic;

namespace OneModel.Compression.Decompression
{
    public class ListResult : IListResult
    {
        public int ExitCode { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public long PhysicalSize { get; set; }

        public IReadOnlyCollection<IListResultItem> Entries { get; set; }
    }
}