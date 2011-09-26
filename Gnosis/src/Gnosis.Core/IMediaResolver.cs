using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaResolver
    {
        IMedia Resolve(IMediaRequest request);
        void ResolveAsync(IMediaRequest request, Action<IMedia> callback);
    }
}
