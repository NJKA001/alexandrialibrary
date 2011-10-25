using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Atom
{
    public interface IAtomTextConstruct
        : IAtomCommon
    {
        string Text { get; }
        AtomTextType Type { get; }
    }
}
