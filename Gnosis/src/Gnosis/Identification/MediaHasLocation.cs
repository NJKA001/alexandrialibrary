using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Identification
{
    public class MediaHasLocation
        : MediaExaminerBase
    {
        protected override bool DoCanExamine(IMediaInfo info)
        {
            return (info.Location != null);
        }

        protected override IMediaInfo DoExamine(IMediaInfo info)
        {
            return info;
        }
    }
}
