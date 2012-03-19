using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public interface IAtomTextConstruct
        : IAtomCommon
    {
        string Text { get; }
        AtomTextType Type { get; }
    }
}
