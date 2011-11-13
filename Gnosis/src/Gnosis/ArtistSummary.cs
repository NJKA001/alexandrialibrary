using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class ArtistSummary
        : IArtistSummary
    {
        public ArtistSummary(string name, Uri image)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.image = image;
        }

        private readonly string name;
        private readonly Uri image;

        public string Name
        {
            get { return name; }
        }

        public Uri Image
        {
            get { return image; }
        }
    }
}
