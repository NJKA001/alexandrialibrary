using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class YouTubeUserFavoritesSource : SourceBase
    {
        public YouTubeUserFavoritesSource()
            : this(Guid.NewGuid())
        {
        }

        public YouTubeUserFavoritesSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/YouTube.png";
            AddChild(new ProxySource(Guid.Empty) { Parent = this });
        }
    }
}
