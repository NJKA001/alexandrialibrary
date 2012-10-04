using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IExhaustableCard
        : ICardInPlay
    {
        bool IsExhausted { get; }
        bool IsReady { get; }

        void Exhaust();
        void Ready();
    }
}
