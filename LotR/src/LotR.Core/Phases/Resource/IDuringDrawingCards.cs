using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Resource
{
    public interface IDuringDrawingCards
    {
        void Setup(IDrawCardsStep step);
        void Resolve(IDrawCardsStep step, IPayment payment);
    }
}
