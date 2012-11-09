using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects.Costs;

namespace LotR.States
{
    public interface ICharacterInPlay
        : ICardInPlay<ICharacterCard>
    {
        bool CanPayFor(ICard card, ICost cost);
    }
}
