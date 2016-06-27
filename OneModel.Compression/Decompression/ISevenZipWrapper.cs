namespace OneModel.Compression.Decompression
{
    public interface ISevenZipWrapper
    {

        /// <summary>
        /// Tests the integrity of an archive.
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        ITestResult Test(string inputPath);

        /// <summary>
        /// Lists the entries in an archive.
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        IListResult List(string inputPath);

        /// <summary>
        /// Extracts files from an archive.
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        IExtractResult Extract(string inputPath, string outputPath);
    }
}