using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IRepresentationLink
    {
        string Content { get; }
        string Rel { get; }
        string Rev { get; }
        IRepresentation Source { get; }
        IRepresentation Target { get; }
    }
}
