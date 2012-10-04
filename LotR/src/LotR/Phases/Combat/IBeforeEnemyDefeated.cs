using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Combat
{
    public interface IBeforeEnemyDefeated
    {
        void BeforeEnemyDefeated(IEnemyDefeatedStep step);
    }
}
