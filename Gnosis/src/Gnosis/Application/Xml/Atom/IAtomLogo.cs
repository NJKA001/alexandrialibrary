using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public interface IAtomLogo
        : IAtomCommon
    {
        Uri Uri { get; }
    }
}
