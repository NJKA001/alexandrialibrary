using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public interface IDefendingInPlay
        : ICardInPlay<IDefendingCard>
    {
        byte Defense { get; set; }
    }
}
