using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;

namespace LotR.Effects
{
    public class EffectOptions
        : IEffectOptions
    {
        public EffectOptions()
            : this(null, null, null)
        {
        }

        public EffectOptions(IChoice choice)
            : this(choice, null, null)
        {
        }

        public EffectOptions(IChoice choice, ICost cost)
            : this(choice, cost, null)
        {
        }

        public EffectOptions(IChoice choice, ILimit limit)
            : this(choice, null, limit)
        {
        }

        public EffectOptions(ICost cost)
            : this(null, cost, null)
        {
        }

        public EffectOptions(ICost cost, ILimit limit)
            : this(null, cost, limit)
        {
        }

        public EffectOptions(IChoice choice, ICost cost, ILimit limit)
        {
            this.choice = choice;
            this.cost = cost;
            this.limit = limit;
        }

        private readonly IChoice choice;
        private readonly ICost cost;
        private readonly ILimit limit;
        private IPayment payment;
        
        public IChoice Choice
        {
            get { return choice; }
        }

        public ICost Cost
        {
            get { return cost; }
        }

        public ILimit Limit
        {
            get { return limit; }
        }

        public IPayment Payment
        {
            get { return payment; }
        }

        public static IEffectOptions Empty = new EffectOptions();

        public void AddPayment(IPayment payment)
        {
            if (payment == null)
                throw new ArgumentNullException("payment");

            this.payment = payment;
        }
    }
}
