using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class RepresentationGraph
        : IRepresentationGraph
    {
        public RepresentationGraph()
        {
        }

        private readonly IList<IRepresentationLink> links = new List<IRepresentationLink>();
        private readonly IList<IRepresentation> representations = new List<IRepresentation>();
        private readonly IDictionary<string, IList<IRepresentation>> bySource = new Dictionary<string, IList<IRepresentation>>();
        private readonly IDictionary<string, IList<IRepresentation>> byTarget = new Dictionary<string, IList<IRepresentation>>();

        public IEnumerable<IRepresentationLink> Links
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IRepresentation> Representations
        {
            get { return representations; }
        }

        public IEnumerable<IRepresentation> Sources
        {
            get { return links.Select(x => x.Source); }
        }

        public IEnumerable<IRepresentation> Targets
        {
            get { return links.Select(x => x.Target); }
        }

        public void AddLink(IRepresentationLink link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            links.Add(link);

            var sourceUrl = link.Source.Location.ToString();
            var targetUrl = link.Target.Location.ToString();

            if (!bySource.ContainsKey(sourceUrl))
                bySource[sourceUrl] = new List<IRepresentation> { link.Target };
            else
                bySource[sourceUrl].Add(link.Target);

            if (!byTarget.ContainsKey(targetUrl))
                byTarget[targetUrl] = new List<IRepresentation> { link.Source };
            else
                byTarget[targetUrl].Add(link.Source);
        }

        public void AddRepresentation(IRepresentation representation)
        {
            if (representation == null)
                throw new ArgumentNullException("representation");

            representations.Add(representation);
        }
    }
}
