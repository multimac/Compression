using System.IO;
using OneModel.TemporaryFolder.Folder;
using Xunit;

namespace OneModel.TemporaryFolder.UnitTests.Folder
{
    public class TempFolderTests
    {
        [Fact]
        public void TempFolder_Deletes_Directory_When_It_Is_Not_Empty()
        {
            string tempFolderPath;
            using (var tempFolder = new TempFolder())
            {
                tempFolderPath = tempFolder.Path;
                File.AppendAllText(tempFolder.CreateUniqueFilename(), "test");
            }

            Assert.False(Directory.Exists(tempFolderPath));
        }
    }
}
