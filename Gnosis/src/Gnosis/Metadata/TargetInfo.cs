using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public struct TargetInfo
    {
        public TargetInfo(Uri location, IContentType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            this.location = location;
            this.type = type;
        }

        private Uri location;
        private IContentType type;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType Type
        {
            get { return type; }
        }

        public static TargetInfo GetDefault(IContentTypeFactory contentTypeFactory)
        {
            return new TargetInfo(Guid.Empty.ToUrn(), contentTypeFactory.Default);
        }
    }
}
