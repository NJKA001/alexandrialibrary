using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public interface IAtomCommon
    {
        Uri BaseId { get; }
        ILanguageTag Lang { get; }
    }
}
