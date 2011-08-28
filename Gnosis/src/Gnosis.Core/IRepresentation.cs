using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IRepresentation
    {
        IResourceLocation Location { get; }
        IMediaType MediaType { get; }
    }
}
