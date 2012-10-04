using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IEffect
    {
        string Description { get; }

        ICost GetCost(IPhaseStep step);
        ILimit GetLimit();
    }
}
