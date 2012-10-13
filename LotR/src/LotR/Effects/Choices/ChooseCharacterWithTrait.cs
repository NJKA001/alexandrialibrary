using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseCharacterWithTrait
        : PlayerChoiceBase, IChooseCharacterWithTrait
    {
        public ChooseCharacterWithTrait(ISource source, IPlayer player, Trait trait)
            : base(GetDescription(trait), source, player)
        {
            this.Trait = trait;
        }

        private static string GetDescription(Trait trait)
        {
            return string.Format("Choose a '{0}' character", trait);
        }

        public Trait Trait
        {
            get;
            private set;
        }

        public ICardInPlay<ICharacterCard> Character
        {
            get;
            set;
        }
    }
}
