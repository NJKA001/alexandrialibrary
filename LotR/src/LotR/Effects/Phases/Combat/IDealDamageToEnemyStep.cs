using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;

namespace LotR.Effects.Phases.Combat
{
    public interface IDealDamageToEnemyStep
        : IPhaseStep
    {
        IEnemyInPlay Target { get; set; }
        byte Damage { get; set; }
    }
}
