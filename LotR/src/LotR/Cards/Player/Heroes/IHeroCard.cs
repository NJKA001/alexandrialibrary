using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Heroes
{
    public interface IHeroCard
        : ICharacterCard, IResourcefulCard
    {
        byte ThreatCost { get; }
    }
}
