using System.IO;
using OneModel.Compression.Decompression;
using OneModel.TemporaryFolder.Folder;
using Xunit;

namespace OneModel.Compression.UnitTests.Decompression.SevenZipWrappers
{
    public class ExtractTests
    {
        public const string ExpectedContents = "1234567890\r\n" +
                                               "qwertyuiop";
        
        [Theory]
        [InlineData("document.7z", "7z", "document.txt")]
        [InlineData("document.rar", "Rar", "document.txt")]
        [InlineData("document.tar", "tar", "document.txt")]
        [InlineData("document.txt.gz", "gzip", "document.txt")]
        [InlineData("document.txt.bz2", "bzip2", "document.txt")]
        [InlineData("document.txt.xz", "xz", "document.txt")]
        [InlineData("document.zip", "zip", "document.txt")]
        public void TestExtract(string archiveName, string expectedArchiveType, string expectedFileName)
        {
            using (var output = new TempFolder())
            {
                var expectedFilePath = $"{output.GetPath()}\\{expectedFileName}";

                var wrapper = new SevenZipWrapper(".\\Dependencies\\7z.exe");
                var result = wrapper.Extract($".\\Decompression\\Inputs\\{archiveName}", output.GetPath());

                Assert.True(result.Ok);
                Assert.Equal(expectedArchiveType, result.Type);
                Assert.Equal(true, File.Exists(expectedFilePath));
                Assert.Equal(ExpectedContents, File.ReadAllText(expectedFilePath));
            }
        }

        [Fact]
        public void TestTarGz()
        {
            using (var gzOutput = new TempFolder())
            using (var tarOutput = new TempFolder())
            {
                var wrapper = new SevenZipWrapper(".\\Dependencies\\7z.exe");
                var unzipped = wrapper.Extract($".\\Decompression\\Inputs\\document.tar.gz", gzOutput.GetPath());
                var untarred = wrapper.Extract(Directory.GetFiles(gzOutput.GetPath())[0], tarOutput.GetPath());
                
                var expectedFilePath = $"{tarOutput.GetPath()}\\document.txt";
                
                Assert.True(unzipped.Ok);
                Assert.Equal("tar", untarred.Type);
                Assert.Equal(true, File.Exists(expectedFilePath));
                Assert.Equal(ExpectedContents, File.ReadAllText(expectedFilePath));
            }
        }
    }
}