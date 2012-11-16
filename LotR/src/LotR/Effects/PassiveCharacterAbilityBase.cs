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
        protected PassiveCharacterAbilityBase(string text, IPlayerCard source)
            : base("Passive", text, source)
        {
        }

        public override string ToString()
        {
            return text;
        }
    }
}
