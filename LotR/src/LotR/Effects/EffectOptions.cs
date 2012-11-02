using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;

namespace LotR.Effects
{
    public struct EffectOptions
    {
        public EffectOptions(IPayment payment, IChoice choice)
        {
            this.payment = payment;
            this.choice = choice;
        }

        private readonly IPayment payment;
        private readonly IChoice choice;

        public IPayment Payment
        {
            get { return payment; }
        }

        public IChoice Choice
        {
            get { return choice; }
        }

        public static EffectOptions Empty = new EffectOptions(null, null);
    }
}
