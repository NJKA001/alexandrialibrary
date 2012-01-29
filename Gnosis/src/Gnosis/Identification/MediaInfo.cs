using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Identification
{
    public class MediaInfo
        : IMediaInfo
    {
        public MediaInfo(Uri location)
            : this(location, null, null, null, null, null, null)
        {
        }

        public MediaInfo(Uri location, IContentType responseContentType, IMediaType locationMediaType, IMediaType magicNumberMediaType, IMediaType contentMediaType, ICharacterSet bomCharacterSet, ICharacterSet contentCharacterSet)
        {
            this.location = location;
            this.responseContentType = responseContentType;
            this.locationMediaType = locationMediaType;
            this.magicNumberMediaType = magicNumberMediaType;
            this.contentMediaType = contentMediaType;
            this.bomCharacterSet = bomCharacterSet;
            this.contentCharacterSet = contentCharacterSet;
        }

        private Uri location;        
        private IContentType responseContentType;
        private IMediaType locationMediaType;
        private IMediaType magicNumberMediaType;
        private IMediaType contentMediaType;
        private ICharacterSet bomCharacterSet;
        private ICharacterSet contentCharacterSet;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType ResponseContentType
        {
            get { return responseContentType; }
        }

        public IMediaType LocationMediaType
        {
            get { return locationMediaType; }
        }

        public IMediaType MagicNumberMediaType
        {
            get { return magicNumberMediaType; }
        }

        public IMediaType ContentMediaType
        {
            get { return contentMediaType; }
        }

        public ICharacterSet BomCharacterSet
        {
            get { return bomCharacterSet; }
        }

        public ICharacterSet ContentCharacterSet
        {
            get { return contentCharacterSet; }
        }
    }
}
