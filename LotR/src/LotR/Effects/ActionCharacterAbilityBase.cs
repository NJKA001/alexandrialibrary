using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class ActionCharacterAbilityBase
        : CharacterAbilityBase, IActionEffect
    {
        protected ActionCharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
        }
    }
}
