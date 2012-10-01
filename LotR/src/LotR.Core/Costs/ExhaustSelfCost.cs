using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Costs
{
    public class ExhaustSelfCost
        : ICost
    {
        public ExhaustSelfCost(IExhaustableCard exhaustable)
        {
            if (exhaustable == null)
                throw new ArgumentNullException("exhaustable");

            this.Description = string.Format("Exhaust {0}", exhaustable.Card.Title);
            this.exhaustable = exhaustable;
        }

        private readonly IExhaustableCard exhaustable;

        public ICard Source
        {
            get { return exhaustable.Card; }
        }

        public string Description
        {
            get;
            private set;
        }

        public bool IsMetBy(IPayment payment)
        {
            var exhaustPayment = payment as IExhaustCardPayment;

            return (exhaustPayment != null && exhaustPayment.Source != null && exhaustPayment.Source.CardId == Source.Id);
        }
    }
}
