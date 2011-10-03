using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Link
        : ILink
    {
        public Link(Uri source, Uri target, ILinkSchema schema, string name)
            : this(source, target, schema, name, 0)
        {
        }

        public Link(Uri source, Uri target, ILinkSchema schema, string name, long id)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");
            if (schema == null)
                throw new ArgumentNullException("schema");
            if (name == null)
                throw new ArgumentNullException("name");

            this.source = source;
            this.target = target;
            this.schema = schema;
            this.name = name;
            this.id = id;
        }

        private readonly long id;
        private readonly Uri source;
        private readonly Uri target;
        private readonly ILinkSchema schema;
        private readonly string name;

        #region ILink Members

        public long Id
        {
            get { return id; }
        }

        public Uri Source
        {
            get { return source; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public ILinkSchema Schema
        {
            get { return schema; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("link name='{0}' target='{1}' source='{2}'", name, target, source);  
        }
    }
}
