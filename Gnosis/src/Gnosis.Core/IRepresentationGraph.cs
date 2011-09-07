using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IRepresentationGraph
    {
        void AddLink(IRepresentationLink link);
        void AddRepresentation(IRepresentation representation);

        IEnumerable<IRepresentationLink> GetLinks();
        IEnumerable<IRepresentation> GetRepresentations();
        IEnumerable<IRepresentation> GetSources();
        IEnumerable<IRepresentation> GetTargets();
    }
}
