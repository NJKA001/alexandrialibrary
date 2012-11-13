using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Effects
{
    public abstract class CharacterAbilityBase
        : CardEffectBase, ICharacterAbility
    {
        protected CharacterAbilityBase(string description, IPlayerCard playerCard)
            : base(GetName(playerCard), description, playerCard)
        {
        }

        private static string GetName(IPlayerCard playerCard)
        {
            return string.Format("{0}'s character ability", playerCard.Title);
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
