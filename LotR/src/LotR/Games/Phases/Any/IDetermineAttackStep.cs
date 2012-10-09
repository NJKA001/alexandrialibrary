using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Games.Phases.Any
{
    public interface IDetermineAttackStep
        : IPhaseStep
    {
        IEnemyInPlay Target { get; }
        IEnumerable<IAttackingCard> Attackers { get; }
        byte Attack { get; set; }
    }
}
