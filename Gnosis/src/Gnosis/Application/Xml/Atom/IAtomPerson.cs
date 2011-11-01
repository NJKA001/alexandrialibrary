using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public interface IAtomPerson
        : IAtomCommon
    {
        string PersonName { get; }
        Uri Uri { get; }
        string Email { get; }
    }
}
