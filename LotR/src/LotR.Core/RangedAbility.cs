using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Combat;

namespace LotR.Core
{
    public class RangedAbility
        : CharacterAbilityBase, IDuringCounterattackDeclared
    {
        public RangedAbility(IPlayerCard source)
            : base(source, "Ranged")
        {
        }
    }
}
