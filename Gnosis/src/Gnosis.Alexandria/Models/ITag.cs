using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public interface ITag
        : IValue
    {
        Uri Scheme { get; }
        string Value { get; }
    }
}
