using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IParameter
    {
        string Name { get; }
        object Value { get; }
    }
}
