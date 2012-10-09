using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Payments;
using LotR.Games;

namespace LotR.Effects.Costs
{
    public class ExhaustSelf
        : CostBase, ICost
    {
        public ExhaustSelf(IExhaustableInPlay exhaustable)
            : base(string.Format("Exhaust {0}", exhaustable.Card.Title), exhaustable.Card)
        {
            this.exhaustable = exhaustable;
        }

        private readonly IExhaustableInPlay exhaustable;

        public override bool IsMetBy(IPayment payment)
        {
            var exhaustPayment = payment as IExhaustCardPayment;

            return (exhaustPayment != null && exhaustPayment.Exhaustable != null && exhaustPayment.Exhaustable.CardId == Source.Id);
        }
    }
}
