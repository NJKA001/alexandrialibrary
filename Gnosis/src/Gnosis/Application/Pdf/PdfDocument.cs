using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Pdf
{
    public class PdfDocument
        : IPdfDocument
    {
        public PdfDocument(Uri location, IContentType type)
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

        public void Load()
        {
        }

        public Uri Location
        {
            get { return location; }
        }

        public IContentType Type
        {
            get { return type; }
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
