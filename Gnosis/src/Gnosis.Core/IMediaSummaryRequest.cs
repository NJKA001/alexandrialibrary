using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaSummaryRequest
    {
        string Pattern { get; }
        Action<IMediaSummary> ItemCallback { get; }
        Action CompletedCallback { get; }
    }
}
