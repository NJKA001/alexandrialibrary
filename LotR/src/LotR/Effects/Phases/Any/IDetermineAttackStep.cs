using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Games;

namespace LotR.Effects.Phases.Any
{
    public interface IDetermineAttackStep
        : IPhaseStep
    {
        IEnemyInPlay Target { get; }
        IEnumerable<IAttackingCard> Attackers { get; }
        byte Attack { get; set; }
    }
}
