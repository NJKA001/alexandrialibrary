using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml.Atom
{
    public interface IAtomContent
        : IAtomTextConstruct
    {
        IMediaType MediaType { get; }
        Uri Src { get; }
    }
}
