using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IContentType
    {
        string Name { get; }
        ICharacterSet CharSet { get; }
        string Boundary { get; }
    }
}
