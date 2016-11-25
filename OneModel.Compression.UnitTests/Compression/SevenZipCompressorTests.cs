using OneModel.Compression.Compression;
using Xunit;

namespace OneModel.Compression.UnitTests.Compression
{
    public class SevenZipCompressorTests : ICompressorTests
    {
        private ICompressor BuildCompressor() => new SevenZipCompressor(@".\Dependencies\7z.exe");

        [Fact]
        public void Handles_Path_With_Spaces() => Handles_Paths_With_Spaces(BuildCompressor());
    }
}
