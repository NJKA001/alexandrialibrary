using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases.Any;

namespace LotR.Cards
{
    public interface IDefendingCard
        : ICard
    {
        void DetermineDefense(IDetermineDefenseStep step);
    }
}
