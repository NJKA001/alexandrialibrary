using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class CharacterAbilityBase
        : CardEffectBase, ICharacterAbility
    {
        protected CharacterAbilityBase(IPlayerCard source, string description)
            : base(source, description)
        {
        }
    }
}
