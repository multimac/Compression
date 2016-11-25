using System.IO;
using OneModel.Compression.Compression;
using OneModel.TemporaryFolder;
using Xunit;

namespace OneModel.Compression.UnitTests.Compression
{
    public class ICompressorTests
    {
        public static string InputFile = @".\\Compression\\Inputs\\document.txt";

        public void Handles_Paths_With_Spaces(ICompressor compressor)
        {
            var filenameWithSpaces = "Test Name With Spaces";
            using (var tempFolder = new TempFolder())
            {
                var path = Path.Combine(tempFolder.GetPath(), filenameWithSpaces);
                var results = compressor.Compress(InputFile, path);

                Assert.True(results.Ok);
            }
        }
    }
}
