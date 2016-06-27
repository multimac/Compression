using System;
using OneModel.Compression.Decompression;
using Xunit;

namespace OneModel.Compression.UnitTests.Decompression.SevenZipWrappers
{
    public class TestTests
    {
        [Theory]
        [InlineData("document.7z", "7z")]
        [InlineData("document.rar", "Rar")]
        [InlineData("document.tar", "tar")]
        [InlineData("document.txt.bz2", "bzip2")]
        [InlineData("document.txt.gz", "gzip")]
        [InlineData("document.txt.xz", "xz")]
        [InlineData("document.zip", "zip")]
        public void An_Archive_Can_Be_Tested(string input, string expectedType)
        {
            var wrapper = new SevenZipWrapper(".\\Dependencies\\7z.exe");
            var result = wrapper.Test($".\\Decompression\\Inputs\\{input}");
            Assert.True(result.Ok);
            Assert.Equal(expectedType, result.Type);
        }

        [Fact]
        public void An_Exception_Is_Thrown_When_Testing_A_Non_Existant_File()
        {
            var wrapper = new SevenZipWrapper(".\\Dependencies");
            Assert.ThrowsAny<Exception>(() => wrapper.Test(".\\Inputs\\document.nosuchfile"));
        }
    }
}