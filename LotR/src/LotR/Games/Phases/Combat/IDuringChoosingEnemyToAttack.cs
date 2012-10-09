using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Games.Phases.Combat
{
    public interface IDuringChoosingEnemyToAttack
    {
        void DuringChoosingEnemyToAttack(IChooseEnemyToAttackStep step);
    }
}
