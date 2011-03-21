using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon.Models
{
    public class PlaylistSource : SourceBase
    {
        public PlaylistSource()
            : this(Guid.NewGuid())
        {
        }

        public PlaylistSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/playlist.png";
        }
    }
}
