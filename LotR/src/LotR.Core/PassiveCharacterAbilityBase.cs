using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class PassiveCharacterAbilityBase
        : CharacterAbilityBase
    {
        protected PassiveCharacterAbilityBase(IPlayerCard source, string description)
            : base(source, description)
        {
        }
    }
}
