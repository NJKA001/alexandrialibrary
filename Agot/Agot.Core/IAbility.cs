using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IAbility
    {
        AbilityType Type { get; }
        PhaseType Phase { get; }
        LimitType Limit { get; }
        string Text { get; }
    }
}
