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
        public ChooseCharacter(ISource source)
            : base("Choose a character", source)
        {
        }

        public ICardInPlay<ICharacterCard> Character
        {
            get;
            set;
        }
    }
}
