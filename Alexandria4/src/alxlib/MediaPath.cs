using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gnosis.Alexandria
{
    public class MediaPath
        : IMediaPath
    {
        public MediaPath(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            this.path = path;
            uri = new Uri(path, UriKind.RelativeOrAbsolute);

            if (uri.IsAbsoluteUri)
            {
                if (uri.IsFile)
                {
                    file = new FileInfo(uri.AbsolutePath);
                    localPath = uri.LocalPath;
                    absolutePath = file.FullName;
                }
                else
                {
                    localPath = null;
                    absolutePath = uri.AbsolutePath;
                }
            }
            else
            {
                try
                {
                    file = new FileInfo(path);
                    localPath = file.FullName;
                    absolutePath = file.FullName;
                }
                catch (Exception)
                {
                    localPath = null;
                    absolutePath = path;
                }
            }

            isLocal = localPath != null;
        }

        private string path;
        private Uri uri;
        private FileInfo file;
        private string localPath;
        private bool isLocal;
        private string absolutePath;

        public bool IsLocal
        {
            get { return isLocal; }
        }

        public string AbsolutePath
        {
            get { return absolutePath; }
        }

        public override string ToString()
        {
            return AbsolutePath;
        }
    }
}
