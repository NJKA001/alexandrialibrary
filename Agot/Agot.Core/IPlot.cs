using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IPlot
        : ICard
    {
        byte Income { get; }
        byte Initiative { get; }
        byte Claim { get; }
    }
}
