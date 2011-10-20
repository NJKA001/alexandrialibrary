using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaDetailRequest
    {
        string Pattern { get; }
        Action<IMediaDetail> ItemCallback { get; }
        Action CompletedCallback { get; }
    }
}
