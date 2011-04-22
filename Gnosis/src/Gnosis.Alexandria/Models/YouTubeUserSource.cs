using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class YouTubeUserSource : SourceBase
    {
        public YouTubeUserSource()
            : this(Guid.NewGuid())
        {
        }

        public YouTubeUserSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/YouTube.png";
            AddChild(new ProxySource(Guid.Empty) { Parent = this });
        }
    }
}
