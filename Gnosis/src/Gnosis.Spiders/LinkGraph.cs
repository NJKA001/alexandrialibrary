using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Spiders
{
    public class LinkGraph
        : ILinkGraph
    {
        public LinkGraph(Uri source, string name, string rel, string rev)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            this.source = source;
            this.name = name;
            this.rel = rel;
            this.rev = rev;
        }

        private readonly Uri source;
        private readonly string name;
        private readonly string rel;
        private readonly string rev;
        private readonly IList<ILinkGraph> children = new List<ILinkGraph>();

        public Uri Source
        {
            get { return source; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Rel
        {
            get { return rel; }
        }

        public string Rev
        {
            get { return rev; }
        }

        public IEnumerable<ILinkGraph> Children
        {
            get { return children; }
        }

        public void AddChild(ILinkGraph child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            children.Add(child);
        }
    }
}
