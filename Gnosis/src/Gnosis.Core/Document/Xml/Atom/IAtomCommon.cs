using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Atom
{
    public interface IAtomCommon
        : IElement
    {
        Uri BaseId { get; }
        ILanguageTag Lang { get; }
    }
}
