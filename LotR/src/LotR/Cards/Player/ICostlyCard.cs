using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Costs;
using LotR.Games.Phases;

namespace LotR.Cards.Player
{
    public interface ICostlyCard
        : IPlayerCard
    {
        Sphere BaseResourceSphere { get; }
        byte BaseResourceCost { get; }
        ICost GetResourceCost(IPhaseStep step);
    }
}
