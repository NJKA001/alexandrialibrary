using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ILimit
    {
        PlayerScope PlayerScope { get; }
        TimeScope TimeScope { get; }
        byte Value { get; }
    }
}
