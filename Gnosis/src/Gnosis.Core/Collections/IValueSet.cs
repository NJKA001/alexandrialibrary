using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public interface IValueSet<T>
        : ISet<T> where T : IValue
    {
    }
}
