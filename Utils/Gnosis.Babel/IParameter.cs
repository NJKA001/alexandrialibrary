using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface IParameter
    {
        string Name { get; }
        object GetValue();
    }
}
