using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ILinkReader
    {
        IEnumerable<ILink> Read(Uri target);
    }
}
