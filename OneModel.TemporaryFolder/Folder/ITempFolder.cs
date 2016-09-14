namespace OneModel.TemporaryFolder.Folder
{
    /// <summary>
    /// For convenience. Creates a unique folder in the temp dir,
    /// and cleans up once disposed.
    /// </summary>
    public interface ITempFolder
    {
        string CreateSubfolder();

        string CreateUniqueFilename();
    }
}