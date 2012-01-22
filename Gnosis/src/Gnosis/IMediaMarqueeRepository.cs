using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaMarqueeRepository
    {
        IMediaMarqueePage GetMarqueePage(MediaCategory category, int pageIndex, int pageSize);
    }
}
