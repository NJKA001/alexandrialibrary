using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class ContentType
        : IContentType
    {
        protected internal ContentType(IMediaType type)
            : this(type, null, null)
        {
        }

        protected internal ContentType(IMediaType type, ICharacterSet charSet)
            : this(type, charSet, null)
        {
        }

        protected internal ContentType(IMediaType type, ICharacterSet charSet, string boundary)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            this.type = type;
            this.charSet = charSet;
            this.boundary = boundary;
        }

        private readonly IMediaType type;
        private readonly ICharacterSet charSet;
        private readonly string boundary;

        #region IContentType Members

        public IMediaType MediaType
        {
            get { return type; }
        }

        public ICharacterSet CharSet
        {
            get { return charSet; }
        }

        public string Boundary
        {
            get { return boundary; }
        }

        public IMedia CreateMedia(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            return type != null ?
                type.CreateMedia(location, this)
                : null;
        }

        #endregion

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(type.ToString());

            if (charSet != null && !charSet.IsDefault)
                builder.AppendFormat("; charset={0}", charSet.ToString());

            if (!string.IsNullOrEmpty(boundary))
                builder.AppendFormat("; boundary={0}", boundary);

            return builder.ToString();
        }
    }
}
