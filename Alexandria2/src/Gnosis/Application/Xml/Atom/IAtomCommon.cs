using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Culture;

namespace Gnosis.Application.Xml.Atom
{
    public interface IAtomCommon
        : IElement
    {
        Uri BaseId { get; }
        ILanguageTag Lang { get; }
    }
}
