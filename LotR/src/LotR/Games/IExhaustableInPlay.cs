using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Games
{
    public interface IExhaustableInPlay
        : ICardInPlay
    {
        bool IsExhausted { get; }
        bool IsReady { get; }

        void Exhaust();
        void Ready();
    }
}
