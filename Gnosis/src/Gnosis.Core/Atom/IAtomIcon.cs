using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Atom
{
    public interface IAtomIcon
        : IAtomCommon
    {
        Uri Location { get; }
    }
}
