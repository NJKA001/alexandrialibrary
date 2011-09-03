using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IRepresentationGraph
    {
        IEnumerable<IRepresentation> Representations { get; }
        IEnumerable<IRepresentationLink> Links { get; }
        IEnumerable<IRepresentation> Sources { get; }
        IEnumerable<IRepresentation> Targets { get; }

        void AddLink(IRepresentationLink link);
        void AddRepresentation(IRepresentation representation);
    }
}
