using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public interface IAtomCommon
        : IXmlElement
    {
        Uri BaseId { get; }
        ILanguageTag Lang { get; }
    }
}
