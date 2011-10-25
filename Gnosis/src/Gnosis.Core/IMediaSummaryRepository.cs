using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaSummaryRepository
    {
        ITask<IMediaSummary> Get(Uri target);
    }
}
