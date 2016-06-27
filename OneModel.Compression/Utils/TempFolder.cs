using System;
using System.IO;

namespace OneModel.Compression.Utils
{

    /// <summary>
    /// For convenience. Creates a unique folder in the temp dir, 
    /// and cleans up once disposed.
    /// </summary>
    public class TempFolder : IDisposable
    {
        private int _nextId;
        private readonly string _path;

        /// <summary>
        /// Creates a unique folder in the temp directory. Calling Dispose()
        /// will clean up the new folder, and anything inside it.
        /// </summary>
        public TempFolder()
        {
            _nextId = 0;
            _path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_path);
        }

        public string Path => _path;
        
        /// <summary>
        /// Creates a uniquely named subfolder.
        /// </summary>
        /// <returns></returns>
        public string CreateSubfolder()
        {
            var path = System.IO.Path.Combine(_path, _nextId.ToString());
            Directory.CreateDirectory(path);
            _nextId++;
            return path;
        }

        /// <summary>
        /// Creates a unique name for a file.
        /// </summary>
        /// <returns></returns>
        public string CreateUniqueFilename()
        {
            return System.IO.Path.Combine(_path, Guid.NewGuid().ToString());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    Directory.Delete(_path);
                }
                catch(Exception)
                {
                    // Suppress error.
                }
            }
        }
    }
}