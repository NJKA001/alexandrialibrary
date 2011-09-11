using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Xspf
{
    public interface IXspfAttribution
        : IXspfElement
    {
        IEnumerable<IXspfAttributable> Items { get; }
    }
}
