using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class YouTubeUserPlaylistsSource : SourceBase
    {
        public YouTubeUserPlaylistsSource()
            : this(Guid.NewGuid())
        {
        }

        public YouTubeUserPlaylistsSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/YouTube.png";
            AddChild(new ProxySource(Guid.Empty) { Parent = this });
        }
    }
}
