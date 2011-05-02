using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class PlaylistItemSource : SourceBase
    {
        public PlaylistItemSource()
            : base()
        {
        }

        public PlaylistItemSource(Guid id)
            : base(id)
        {
        }
    }
}
