using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.CharacterAbilities
{
    public abstract class ActionCharacterAbilityBase
        : CharacterAbilityBase, IAction
    {
        protected ActionCharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
        }
    }
}
