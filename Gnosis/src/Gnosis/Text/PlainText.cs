using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Text
{
    public class PlainText
        : IText
    {
        public PlainText(Uri location, IContentType type)
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

        private string body;
        private bool isLoaded;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType Type
        {
            get { return type; }
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
