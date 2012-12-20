using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.States
{
    public interface IExhaustableInPlay
        : ICardInPlay
    {
        bool IsExhausted { get; }

        void Exhaust();
        void Ready();
    }
}
