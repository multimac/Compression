using OneModel.Compression.Compression;
using Xunit;

namespace OneModel.Compression.UnitTests.Compression
{
    public class LzoCompressorTests : ICompressorTests
    {
        private ICompressor BuildCompressor() => new LzoCompressor(@".\Dependencies\lzop.exe");

        [Fact]
        public void Handles_Path_With_Spaces() => Handles_Paths_With_Spaces(BuildCompressor());
    }
}
