using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Image
{
    public abstract class ImageBase
        : IImage
    {
        protected ImageBase(Uri location, IContentType contentType)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (contentType == null)
                throw new ArgumentNullException("contentType");

            this.location = location;
            this.contentType = contentType;
        }

        private Uri location;
        private IContentType contentType;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType ContentType
        {
            get { return contentType; }
        }
    }
}
