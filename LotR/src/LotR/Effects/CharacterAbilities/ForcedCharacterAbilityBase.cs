using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.CharacterAbilities
{
    public abstract class ForcedCharacterAbilityBase
        : PassiveCharacterAbilityBase, IForced
    {
        protected ForcedCharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
        }
    }
}
