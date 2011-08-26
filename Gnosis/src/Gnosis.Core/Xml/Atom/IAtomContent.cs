using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public interface IAtomContent
        : IAtomTextConstruct
    {
        IMediaType MediaType { get; }
        Uri Src { get; }
    }
}
