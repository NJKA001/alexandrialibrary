using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter.Enemies;

namespace LotR.Games.Phases.Combat
{
    public interface IEnemyDefeatedStep
        : IPhaseStep
    {
        IEnumerable<IAttackingCard> Attackers { get; }
        IEnumerable<IDefendingCard> Defenders { get; }
        IEnemyCard Enemy { get; }
    }
}
