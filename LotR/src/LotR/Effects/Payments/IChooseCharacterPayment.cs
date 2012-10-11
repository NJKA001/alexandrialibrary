using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Payments
{
    public interface IChooseCharacterPayment
        : IPayment
    {
        ICardInPlay<ICharacterCard> Character { get; }
    }
}
