using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMarqueePage
    {
        IEnumerable<IMarquee> Items { get; }
        int NumberOfPages { get; }
        int PageIndex { get; }
        int PageSize { get; }
    }
}
