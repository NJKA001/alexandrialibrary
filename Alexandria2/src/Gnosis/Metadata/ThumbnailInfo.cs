using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public struct ThumbnailInfo
    {
        public ThumbnailInfo(Uri location, byte[] data)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (data == null)
                throw new ArgumentNullException("data");

            this.location = location;
            this.data = data;
        }

        private Uri location;
        private byte[] data;

        public Uri Location
        {
            get { return location; }
        }

        public byte[] Data
        {
            get { return data; }
        }

        public object ToImageSource()
        {
            return data.Length > 0 ? data : (object)location;
        }

        public static readonly ThumbnailInfo Default = new ThumbnailInfo(Guid.Empty.ToUrn(), new byte[0]);
    }
}
