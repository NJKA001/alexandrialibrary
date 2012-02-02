using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Links;

namespace Gnosis.Application.Vendor
{
    public class GnosisFilesystemDirectory
        : IApplication
    {
        public GnosisFilesystemDirectory(Uri location, IContentType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            this.location = location;
            this.type = type;
        }

        private readonly Uri location;
        private readonly IContentType type;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType Type
        {
            get { return type; }
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
                links.Add(new Link(location, new Uri(directory.FullName), type.Name, directory.Name));

            foreach (var file in info.GetFiles())
                links.Add(new Link(location, new Uri(file.FullName), "application/vnd.gnosis.fs-file", file.Name));

            return links;
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }
    }
}
