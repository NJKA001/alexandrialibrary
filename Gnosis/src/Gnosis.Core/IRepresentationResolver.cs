using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IRepresentationResolver
    {
        IRepresentation Resolve(IRepresentationRequest request);
        void ResolveAsync(IRepresentationRequest request, Action<IRepresentation> callback);
    }
}
