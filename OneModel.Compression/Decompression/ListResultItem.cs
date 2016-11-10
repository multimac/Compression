using System;

namespace OneModel.Compression.Decompression
{
    public class ListResultItem : IListResultItem
    {
        public DateTime DateTime { get; set; }

        public string Attr { get; set; }

        public long Size { get; set; }

        public long Compressed { get; set; }

        public string Name { get; set; }
    }
}