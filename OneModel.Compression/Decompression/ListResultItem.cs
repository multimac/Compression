using System;

namespace OneModel.Compression.Decompression
{
    public class ListResultItem : IListResultItem
    {
        public DateTime DateTime { get; set; }

        public string Attr { get; set; }

        public int Size { get; set; }

        public int Compressed { get; set; }

        public string Name { get; set; }
    }
}