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
        protected CharacterAbilityBase(string description, IPlayerCard source)
            : base(description, source)
        {
            this.CardSource = source;
            this.Source = source;
        }

        protected IPlayerCard CardSource
        {
            get;
            private set;
        }

        public new IPlayerCard Source
        {
            get;
            private set;
        }
    }
}
