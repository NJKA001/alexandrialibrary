using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Links
{
    public class Link
        : ILink
    {
        public Link(Uri source, Uri target, string relationship, string name)
            : this(source, target, relationship, name, 0)
        {
        }

        public Link(Uri source, Uri target, string relationship, string name, long id)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");
            if (relationship == null)
                throw new ArgumentNullException("relationship");
            if (name == null)
                throw new ArgumentNullException("name");

            this.source = source;
            this.target = target;
            this.relationship = relationship;
            this.name = name;
            this.id = id;
        }

        private readonly long id;
        private readonly Uri source;
        private readonly Uri target;
        private readonly string relationship;
        private readonly string name;

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

        public string Relationship
        {
            get { return relationship; }
        }

        public string Name
        {
            get { return name; }
        }

        public override string ToString()
        {
            return string.Format("link name='{0}' target='{1}' source='{2}'", name, target, source);  
        }
    }
}
