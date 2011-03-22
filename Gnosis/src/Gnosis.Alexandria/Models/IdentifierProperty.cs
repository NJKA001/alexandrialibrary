using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon.Models
{
    public class IdentifierProperty : SourcePropertyBase<Uri>
    {
        public IdentifierProperty(ISource source)
            : this(Guid.NewGuid(), source)
        {
        }

        public IdentifierProperty(Guid id, ISource source)
            : base(id, source, "Identifier", new Uri("urn:unknown"))
        {
        }
    }
}
