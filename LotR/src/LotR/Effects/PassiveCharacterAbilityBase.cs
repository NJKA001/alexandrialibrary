using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Effects
{
    public abstract class PassiveCharacterAbilityBase
        : CharacterAbilityBase
    {
        protected PassiveCharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
