using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public class MediaSource : SourceBase
    {
        public MediaSource()
            : base()
        {
        }

        public MediaSource(Guid id)
            : base(id)
        {
        }

        public override void AddChild(ISource child)
        {
        }
    }
}
