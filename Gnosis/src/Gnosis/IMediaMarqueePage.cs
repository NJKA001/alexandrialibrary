using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaMarqueePage
    {
        IEnumerable<IMediaMarquee> Items { get; }
        int NumberOfPages { get; }
        int PageIndex { get; }
        int PageSize { get; }
    }
}
