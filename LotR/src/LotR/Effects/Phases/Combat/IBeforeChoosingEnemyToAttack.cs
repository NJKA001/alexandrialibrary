using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Combat
{
    public interface IBeforeChoosingEnemyToAttack
    {
        void BeforeChoosingEnemyToAttack(IGame game);
    }
}
