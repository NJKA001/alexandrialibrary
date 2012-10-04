using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.CharacterAbilities
{
    public abstract class PassiveCharacterAbilityBase
        : CharacterAbilityBase
    {
        protected PassiveCharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
        }
    }
}
