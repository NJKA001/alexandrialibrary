using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public struct TargetInfo
    {
        public TargetInfo(Uri location, IMediaType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            this.location = location;
            this.type = type;
        }

        private Uri location;
        private IMediaType type;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        public static readonly TargetInfo Default = new TargetInfo(Guid.Empty.ToUrn(), MediaType.ApplicationUnknown);
    }
}
