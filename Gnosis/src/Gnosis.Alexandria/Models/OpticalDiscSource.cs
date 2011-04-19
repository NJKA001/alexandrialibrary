using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class OpticalDiscSource : SourceBase
    {
        public OpticalDiscSource()
            : this(Guid.NewGuid())
        {
        }

        public OpticalDiscSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/cd.png";
            AddChild(new ProxySource(Guid.Empty) { Parent = this });
        }
    }
}
