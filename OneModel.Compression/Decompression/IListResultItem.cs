using System;

namespace OneModel.Compression.Decompression
{
    public interface IListResultItem
    {
        DateTime DateTime { get; }

        string Attr { get; }

        int Size { get; }

        int Compressed { get; }

        string Name { get; }
    }
}