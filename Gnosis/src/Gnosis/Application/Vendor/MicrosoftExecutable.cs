using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class MicrosoftExecutable
        : IApplication
    {
        public MicrosoftExecutable(Uri location)
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
            get { return MediaType.ApplicationMicrosoftExecutable; }
        }

        public void Load()
        {
        }

        public IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }
    }
}
