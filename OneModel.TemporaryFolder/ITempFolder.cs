using System;

namespace OneModel.TemporaryFolder
{
    /// <summary>
    /// For convenience. Creates a unique folder in the temp dir,
    /// and cleans up once disposed.
    /// </summary>
    public interface ITempFolder: IDisposable
    {
        /// <summary>
        /// Gets the path location of the temporary folder.
        /// </summary>
        string GetPath();

        /// <summary>
        /// Creates a uniquely named subfolder.
        /// </summary>
        string CreateSubfolder();

        /// <summary>
        /// Creates a unique name for a file.
        /// </summary>
        /// <returns></returns>
        string CreateUniqueFilename();
    }
}