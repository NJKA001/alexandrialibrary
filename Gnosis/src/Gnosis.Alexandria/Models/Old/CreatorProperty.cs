using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class CreatorProperty : SourcePropertyBase<string>
    {
        public CreatorProperty(ISource source)
            : this(Guid.NewGuid(), source)
        {
        }

        public CreatorProperty(Guid id, ISource source)
            : base(id, source, "Creator")
        {
        }
    }
}
