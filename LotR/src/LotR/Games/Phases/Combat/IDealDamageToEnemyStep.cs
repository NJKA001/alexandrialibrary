using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Games.Phases.Combat
{
    public interface IDealDamageToEnemyStep
        : IPhaseStep
    {
        IEnemyInPlay Target { get; set; }
        byte Damage { get; set; }
    }
}
