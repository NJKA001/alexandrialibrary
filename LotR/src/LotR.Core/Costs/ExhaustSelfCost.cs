using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Costs
{
    public class ExhaustSelfCost
        : CostBase, ICost
    {
        public ExhaustSelfCost(IExhaustableCard exhaustable)
            : base(string.Format("Exhaust {0}", exhaustable.Card.Title), exhaustable.Card)
        {
            this.exhaustable = exhaustable;
        }

        private readonly IExhaustableCard exhaustable;

        public override bool IsMetBy(IPayment payment)
        {
            var exhaustPayment = payment as IExhaustCardPayment;

            return (exhaustPayment != null && exhaustPayment.Source != null && exhaustPayment.Source.CardId == Source.Id);
        }
    }
}
