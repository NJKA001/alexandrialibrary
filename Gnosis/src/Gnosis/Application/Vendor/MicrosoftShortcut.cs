using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class MicrosoftShortcut
        : IApplication
    {
        public MicrosoftShortcut(Uri location)
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
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationMicrosoftShortcut; }
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
