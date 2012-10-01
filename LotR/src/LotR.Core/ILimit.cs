using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ILimit
    {
        Duration Scope { get; }
        byte Value { get; }
    }
}
