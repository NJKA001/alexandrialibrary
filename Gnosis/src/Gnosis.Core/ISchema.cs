using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ISchema
    {
        long LocalId { get; }
        Uri Identifier { get; }
        string Name { get; }
    }
}
