using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Links
{
    public class LinkType
        : ILinkType
    {
        public LinkType(int id, string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.id = id;
            this.name = name;
        }

        private readonly int id;
        private readonly string name;

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public static readonly ILinkType Default = new LinkType(1, "Default");
        public static readonly ILinkType ThumbnailImage = new LinkType(2, "ThumbnailImage");
        public static readonly ILinkType Directory = new LinkType(21, "Directory");
        public static readonly ILinkType File = new LinkType(22, "File");
    }
}
