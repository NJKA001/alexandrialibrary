using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChooseCharacter
        : IChoice
    {
        ICardInPlay<ICharacterCard> Character { get; set; }
    }
}
