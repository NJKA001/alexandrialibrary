using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Payments
{
    public interface IChooseCardDestinationPayment
        : IPayment
    {
        bool BottomOfDeck { get; set; }
    }
}
