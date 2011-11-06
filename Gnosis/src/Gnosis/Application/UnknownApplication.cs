using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class UnknownApplication
        : IApplication
    {
        public UnknownApplication(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            this.location = location;
        }

        private readonly Uri location;

        #region IApplication Members

        public void Load()
        {
        }

        #endregion

        #region IMedia Members

        public Uri Location
        {
            get { throw new NotImplementedException(); }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationUnknown; }
        }

        public IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }

        #endregion
    }
}
