using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon.Models
{
    public class LocationProperty : SourcePropertyBase<Uri>
    {
        public LocationProperty(Guid id, ISource source)
            : base(id, source, "Location", new Uri("file:///"))
        {
        }
    }
}
