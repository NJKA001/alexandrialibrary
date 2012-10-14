using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseCharacter
        : ChoiceBase, IChooseCharacter
    {
        public ChooseCharacter(ISource source, IPlayer player)
            : base("Choose a character", source, player)
        {
        }

        public ICardInPlay<ICharacterCard> Character
        {
            get;
            set;
        }
    }
}
