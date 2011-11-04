using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Text
{
    public class PlainText
        : IText
    {
        public PlainText(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            this.location = location;
        }

        private readonly Uri location;
        private string body;
        private bool isLoaded;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.TextPlain; }
        }

        public string Body
        {
            get { return body; }
        }

        public IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }

        public void Load()
        {
            if (!isLoaded)
            {
                isLoaded = true;
                body = location.ToContentString();
            }
        }
    }
}
