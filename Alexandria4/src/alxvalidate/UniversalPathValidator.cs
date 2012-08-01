using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gnosis.Alexandria.Validation
{
    public class UniversalPathValidator
        : IPathValidator
    {
        private string GetLocalPath(Uri path)
        {
            if (path.IsAbsoluteUri)
            {
                return path.IsFile ? path.LocalPath : null;
            }
            else
            {
                try
                {
                    var file = new FileInfo(path.ToString());
                    return file.FullName;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public PathValidation Validate(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            var location = new Uri(path, UriKind.RelativeOrAbsolute);

            var localPath = GetLocalPath(location);
            IPathValidator validator = null;

            if (localPath != null)
            {
                validator = new LocalPathValidator();
                return validator.Validate(localPath);
            }
            else
            {
                validator = new RemotePathValidator();
                return validator.Validate(path.ToString());
            }
        }
    }
}
