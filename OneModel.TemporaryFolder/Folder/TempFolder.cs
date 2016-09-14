using System;
using System.IO;

namespace OneModel.TemporaryFolder.Folder
{
    /// <summary>
    /// For convenience. Creates a unique folder in the temp dir,
    /// and cleans up once disposed.
    /// </summary>
    public class TempFolder : ITempFolder
    {
        private int _nextId;
        private readonly string _path;

        public string GetPath()
        {
            return _path;
        }
        
        /// <summary>
        /// Creates a unique folder in the temp directory. Calling Dispose()
        /// will clean up the new folder, and anything inside it.
        /// </summary>
        public TempFolder()
        {
            _nextId = 0;
            _path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_path);
        }

        /// <summary>Creates a uniquely named subfolder.</summary>
        /// <returns></returns>
        public string CreateSubfolder()
        {
            var path = System.IO.Path.Combine(_path, _nextId.ToString());
            Directory.CreateDirectory(path);
            _nextId = _nextId + 1;
            return path;
        }

        /// <summary>Creates a unique name for a file.</summary>
        /// <returns></returns>
        public string CreateUniqueFilename()
        {
            return Path.Combine(_path, Guid.NewGuid().ToString());
        }

        public void Dispose()
        {
            try
            {
                Directory.Delete(_path, true);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
