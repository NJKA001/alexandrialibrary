using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaRequest
    {
        IEnumerable<ITag> Tags { get; }
        Action<IMedia> ItemFoundCallback { get; }
        Action CompletedCallback { get; }
    }
}
