using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class RepresentationFactory
        : IRepresentationFactory
    {
        public IRepresentation Create(IResourceLocation location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            //var contentType = ContentType.GetContentType(location);
            //if (contentType == null || contentType == ContentType.Empty || contentType.Type == null)
            //    return null;

            //switch (contentType.Type.ToString())
            //{
            //}

            return null;
        }
    }
}
