using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class DeviceCatalogSource : SourceBase
    {
        public DeviceCatalogSource()
            : this(Guid.NewGuid())
        {
        }

        public DeviceCatalogSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/gear.png";
            AddChild(new ProxySource(Guid.Empty) { Parent = this });
        }
    }
}
