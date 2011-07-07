using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Atom
{
    public interface IAtomCategory
        : IAtomCommon
    {
        string Term { get; }

        Uri Scheme { get; }
        string Label { get; }
    }
}
