using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class ContentType
        : IContentType
    {
        protected internal ContentType(string name)
            : this(name, null, null)
        {
        }

        protected internal ContentType(string name, string charSet)
            : this(name, charSet, null)
        {
        }

        protected internal ContentType(string name, string charSet, string boundary)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.charSet = charSet;
            this.boundary = boundary;
        }

        private readonly string name;
        private readonly string charSet;
        private readonly string boundary;

        public string Name
        {
            get { return name; }
        }

        public string CharSet
        {
            get { return charSet; }
        }

        public string Boundary
        {
            get { return boundary; }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(name);

            if (!string.IsNullOrEmpty(charSet))
                builder.AppendFormat("; charset={0}", charSet.ToString());

            if (!string.IsNullOrEmpty(boundary))
                builder.AppendFormat("; boundary={0}", boundary);

            return builder.ToString();
        }
    }
}
