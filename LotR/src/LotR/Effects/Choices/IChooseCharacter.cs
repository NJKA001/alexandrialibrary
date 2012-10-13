using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChooseCharacter
        : IPlayerChoice
    {
        ICardInPlay<ICharacterCard> Character { get; set; }
    }
}
