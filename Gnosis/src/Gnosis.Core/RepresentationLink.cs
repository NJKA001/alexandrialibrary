using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class RepresentationLink
        : IRepresentationLink
    {
        public RepresentationLink(string content, string rel, string rev, IRepresentation source, IRepresentation target)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            this.content = content;
            this.rel = rel;
            this.rev = rev;
            this.source = source;
            this.target = target;
        }

        private readonly string content;
        private readonly string rel;
        private readonly string rev;
        private readonly IRepresentation source;
        private readonly IRepresentation target;

        public string Content
        {
            get { return content; }
        }

        public string Rel
        {
            get { return rel; }
        }

        public string Rev
        {
            get { return rev; }
        }

        public IRepresentation Source
        {
            get { return source; }
        }

        public IRepresentation Target
        {
            get { return target; }
        }
    }
}
