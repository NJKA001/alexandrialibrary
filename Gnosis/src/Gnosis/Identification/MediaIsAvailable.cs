using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Identification
{
    public class MediaIsAvailable
        : MediaHasLocation
    {
        protected override IMediaInfo DoExamine(IMediaInfo info)
        {
            if (info.Location.IsFile)
            {
                if (System.IO.Directory.Exists(info.Location.LocalPath))
                {
                }
                else if (!System.IO.File.Exists(info.Location.LocalPath))
                {
                }
                else
                    return info;
            }
            else
            {
            }

            return info;
        }
    }
}
