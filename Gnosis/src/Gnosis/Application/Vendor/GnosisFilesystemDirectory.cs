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

        public void Load()
        {
        }

        public IEnumerable<ILink> GetLinks()
        {
            var info = (location.IsFile && Directory.Exists(location.LocalPath)) ?
                new DirectoryInfo(location.LocalPath)
                : null;

            if (info == null)
                return Enumerable.Empty<ILink>();

            var links = new List<ILink>();

            foreach (var directory in info.GetDirectories())
                links.Add(new Link(location, new Uri(directory.FullName), LinkType.Directory, directory.Name));

            foreach (var file in info.GetFiles())
                links.Add(new Link(location, new Uri(file.FullName), LinkType.File, file.Name));

            return links;
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }
    }
}
