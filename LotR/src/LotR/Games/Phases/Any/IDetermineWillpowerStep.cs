using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Games.Phases.Any
{
    public interface IDetermineWillpowerStep
        : IPhaseStep
    {
        IWillpowerfulCard Source { get; }
        byte Willpower { get; set; }
    }
}
