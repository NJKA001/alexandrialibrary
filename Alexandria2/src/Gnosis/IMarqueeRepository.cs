using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMarqueeRepository
    {
        IMarqueePage GetMarqueePage(MetadataCategory category, int pageIndex, int pageSize);
    }
}
