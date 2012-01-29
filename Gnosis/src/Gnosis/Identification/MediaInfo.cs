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

        public MediaInfo(Uri location, string fileExtension, IContentType responseContentType, ICharacterSet bomCharacterSet, byte[] contentMagicNumber, IMediaType contentMediaType, ICharacterSet contentCharacterSet)
        {
            this.location = location;
            this.fileExtension = fileExtension;
            this.responseContentType = responseContentType;
            this.bomCharacterSet = bomCharacterSet;
            this.contentMagicNumber = contentMagicNumber;
            this.contentMediaType = contentMediaType;
            this.contentCharacterSet = contentCharacterSet;
        }

        private Uri location;
        private string fileExtension;
        private IContentType responseContentType;
        private ICharacterSet bomCharacterSet;
        private byte[] contentMagicNumber;
        private IMediaType contentMediaType;
        private ICharacterSet contentCharacterSet;

        public Uri Location
        {
            get { return location; }
        }

        public string FileExtension
        {
            get { return fileExtension; }
        }

        public IContentType ResponseContentType
        {
            get { return responseContentType; }
        }

        public ICharacterSet BomCharacterSet
        {
            get { return bomCharacterSet; }
        }

        public byte[] ContentMagicNumber
        {
            get { return contentMagicNumber; }
        }

        public IMediaType ContentMediaType
        {
            get { return contentMediaType; }
        }

        public ICharacterSet ContentCharacterSet
        {
            get { return contentCharacterSet; }
        }

        public IContentType ToContentType()
        {
            throw new NotImplementedException();
        }
    }
}
