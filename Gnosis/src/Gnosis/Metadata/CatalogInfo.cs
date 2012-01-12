using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public struct CatalogInfo
    {
        public CatalogInfo(Uri location, string name)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (name == null)
                throw new ArgumentNullException("name");

            this.location = location;
            this.name = name;
        }

        private Uri location;
        private string name;

        public Uri Location
        {
            get { return location; }
        }

        public string Name
        {
            get { return name; }
        }

        public static readonly CatalogInfo Default = new CatalogInfo(Guid.Empty.ToUrn(), "Unknown Catalog");
    }
}
