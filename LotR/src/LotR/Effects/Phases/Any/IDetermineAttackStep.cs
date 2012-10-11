using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter.Enemies;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public interface IDetermineAttackStep
    {
        ICardInPlay<IEnemyCard> Target { get; }
        IEnumerable<IAttackingCard> Attackers { get; }
        byte Attack { get; set; }
    }
}
