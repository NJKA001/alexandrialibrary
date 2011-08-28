using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Image
{
    public abstract class ImageBase
        : IImage
    {
        protected ImageBase(IMediaType mediaType, IResourceLocation location)
        {
            if (mediaType == null)
                throw new ArgumentNullException("mediaType");

            this.mediaType = mediaType;
            this.location = location;
        }

        protected IResourceLocation location;
        private IMediaType mediaType;

        public IResourceLocation Location
        {
            get { return location; }
        }

        public IMediaType MediaType
        {
            get { return mediaType; }
        }
    }
}
