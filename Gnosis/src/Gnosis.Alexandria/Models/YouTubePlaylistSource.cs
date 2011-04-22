using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class YouTubePlaylistSource : SourceBase
    {
        public YouTubePlaylistSource()
            : this(Guid.NewGuid())
        {
        }

        public YouTubePlaylistSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/YouTube.png";
            AddChild(new ProxySource(Guid.Empty) { Parent = this });
        }
    }
}
