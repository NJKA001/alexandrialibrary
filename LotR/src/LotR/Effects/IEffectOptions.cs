using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;

namespace LotR.Effects
{
    public interface IEffectOptions
    {
        IChoice Choice { get; }
        ICost Cost { get; }
        ILimit Limit { get; }
        IPayment Payment { get; }

        void AddPayment(IPayment payment);
    }
}
