using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Costs
{
    public class ExhaustSelf
        : CostBase, ICost
    {
        public ExhaustSelf(ICardInPlay<IExhaustableCard> exhaustable)
            : base(string.Format("Exhaust {0}", exhaustable.Card.Title), exhaustable.Card)
        {
            this.exhaustable = exhaustable;
        }

        private readonly ICardInPlay<IExhaustableCard> exhaustable;

        public override bool IsMetBy(IPayment payment)
        {
            var exhaustPayment = payment as IExhaustCardPayment;

            return (exhaustPayment != null && exhaustPayment.Exhaustable != null && exhaustPayment.Exhaustable.Card.Id == Source.Id);
        }
    }
}
