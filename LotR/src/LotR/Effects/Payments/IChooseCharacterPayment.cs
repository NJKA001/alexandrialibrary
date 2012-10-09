using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;

namespace LotR.Effects.Payments
{
    public interface IChooseCharacterPayment
        : IPayment
    {
        ICharacterInPlay Character { get; }
    }
}
