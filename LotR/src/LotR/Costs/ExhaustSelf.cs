using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Payments;

namespace LotR.Core.Costs
{
    public class ExhaustSelf
        : CostBase, ICost
    {
        public ExhaustSelf(IExhaustableCard exhaustable)
            : base(string.Format("Exhaust {0}", exhaustable.Card.Title), exhaustable.Card)
        {
            this.exhaustable = exhaustable;
        }

        private readonly IExhaustableCard exhaustable;

        public override bool IsMetBy(IPayment payment)
        {
            var exhaustPayment = payment as IExhaustCardPayment;

            return (exhaustPayment != null && exhaustPayment.Exhaustable != null && exhaustPayment.Exhaustable.CardId == Source.Id);
        }
    }
}
