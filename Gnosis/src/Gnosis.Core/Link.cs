using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Link
        : ILink
    {
        public Link(Uri source, Uri target, Uri type, string name)
            : this(source, target, type, name, 0)
        {
        }

        public Link(Uri source, Uri target, Uri type, string name, long id)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");

            this.source = source;
            this.target = target;
            this.type = type;
            this.name = name;
            this.id = id;
        }

        private readonly long id;
        private readonly Uri source;
        private readonly Uri target;
        private readonly Uri type;
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

        public Uri Type
        {
            get { return type; }
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
