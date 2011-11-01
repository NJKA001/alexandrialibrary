using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Links;

namespace Gnosis.Application.Vendor
{
    public class GnosisFilesystemDirectory
        : IMedia
    {
        public GnosisFilesystemDirectory(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            this.location = location;
        }

        private readonly Uri location;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisFilesystemDirectory; }
        }

        public IEnumerable<ILink> GetLinks()
        {
            var links = new List<ILink>();

            if (!location.IsFile || !Directory.Exists(location.LocalPath))
                return links;

            var current = new DirectoryInfo(location.LocalPath);

            foreach (var directory in current.GetDirectories())
                links.Add(new Link(location, new Uri(directory.FullName), LinkType.Directory, directory.Name));

            foreach (var file in current.GetFiles())
                links.Add(new Link(location, new Uri(file.FullName), LinkType.File, file.Name));

            return links;
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }
    }
}
