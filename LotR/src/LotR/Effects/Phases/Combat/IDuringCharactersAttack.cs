using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Phases.Combat
{
    public interface IDuringCharactersAttack
    {
        void DuringCharactersAttack(ICharactersAttackStep step);
    }
}
