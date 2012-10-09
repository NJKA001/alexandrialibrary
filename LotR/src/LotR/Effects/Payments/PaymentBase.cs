using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Payments
{
    public abstract class PaymentBase
        : IPayment
    {
        protected PaymentBase(string description)
        {
            this.Description = description;
        }

        public string Description
        {
            get;
            private set;
        }
    }
}
