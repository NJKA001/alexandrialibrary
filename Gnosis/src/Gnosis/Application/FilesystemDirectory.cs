using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Links;

namespace Gnosis.Application
{
    public class FilesystemDirectory
        : IMedia
    {
        public FilesystemDirectory(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (!location.IsFile)
                throw new ArgumentException("location must be a file url");

            this.location = location;
        }

        private readonly Uri location;

        #region IMedia Members

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationFilesystemDirectory; }
        }

        public IEnumerable<ILink> GetLinks()
        {
            var links = new List<ILink>();

            if (!Directory.Exists(location.LocalPath))
            {
                System.Diagnostics.Debug.WriteLine("FSDir does not exist: " + location.LocalPath);
                return links;
            }

            var targetDirectory = new DirectoryInfo(location.LocalPath);

            foreach (var file in targetDirectory.GetFiles())
            {
                links.Add(new Link(location, new Uri(file.FullName), LinkType.File, file.Name));
            }

            foreach (var directory in targetDirectory.GetDirectories())
            {
                links.Add(new Link(location, new Uri(directory.FullName), LinkType.Directory, directory.Name));
            }

            return links;
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }

        #endregion
    }
}
