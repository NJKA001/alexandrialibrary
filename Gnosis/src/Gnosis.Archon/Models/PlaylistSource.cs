using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon.Models
{
    public class PlaylistSource : SourceBase
    {
        public PlaylistSource()
            : base()
        {
        }

        public PlaylistSource(Guid id)
            : base(id)
        {
        }
    }
}
