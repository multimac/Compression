using System;

namespace OneModel.Compression.Decompression
{
    public interface IListResultItem
    {
        DateTime DateTime { get; }

        string Attr { get; }

        long Size { get; }

        long Compressed { get; }

        string Name { get; }
    }
}