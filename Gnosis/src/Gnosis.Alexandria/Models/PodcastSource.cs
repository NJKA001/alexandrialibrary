using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class PodcastSource : SourceBase
    {
        public PodcastSource()
            : this(Guid.NewGuid())
        {
        }

        public PodcastSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/podcast.png";
            AddChild(new ProxySource(Guid.Empty){Parent = this});
        }
    }
}
