using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Games.Phases.Combat
{
    public interface IDuringEnemyDefeated
    {
        void DuringEnemyDefeated(IEnemyDefeatedStep step);
    }
}
