using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Atom
{
    public interface IAtomEntry
    {
        string Title { get; }
        Uri Link { get; }
        Uri Id { get; }
        DateTime Updated { get; }
        string Summary { get; }

        
    }
}
