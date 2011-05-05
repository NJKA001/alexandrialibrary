using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface IHashCode
        : IValue
    {
        Uri Scheme { get; }
        string Value { get; }
    }
}
