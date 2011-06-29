using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Ietf
{
    public interface IAtomPerson
    {
        string Name { get; }
        Uri Url { get; }
        Uri Email { get; }
    }
}
