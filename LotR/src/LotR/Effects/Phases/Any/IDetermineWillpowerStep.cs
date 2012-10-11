using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects.Phases.Any
{
    public interface IDetermineWillpowerStep
    {
        IWillpowerfulCard Source { get; }
        byte Willpower { get; set; }
    }
}
