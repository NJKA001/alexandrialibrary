using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Costs
{
    public class PayResourcesFrom
        : CostBase, IPayResourcesFrom
    {
        public PayResourcesFrom(ISource source, ICharacterInPlay target, byte numberOfResources, bool isVariableCost)
            : base(GetDescription(target, numberOfResources), source)
        {
            this.target = target;
            this.numberOfResources = numberOfResources;
            this.isVariableCost = isVariableCost;
        }

        private static string GetDescription(ICardInPlay target, byte numberOfResources)
        {
            return numberOfResources == 1 ?
                string.Format("Pay 1 resource from {0}'s pool", target.Title)
                : string.Format("Pay {0} resources from {1}'s pool", numberOfResources, target.Title);
        }

        private readonly ICharacterInPlay target;
        private readonly byte numberOfResources;
        private readonly bool isVariableCost;

        public ICharacterInPlay Target
        {
            get { return target; }
        }

        public byte NumberOfResources
        {
            get { return numberOfResources; }
        }

        public bool IsVariableCost
        {
            get { return isVariableCost; }
        }

        public override bool IsMetBy(IPayment payment)
        {
            if (payment == null)
                return false;

            var resourcePayment = payment as IResourcePayment;
            if (resourcePayment == null)
                return false;

            if (resourcePayment.Payments.Count() != 1)
                return false;

            var firstPayment = resourcePayment.Payments.FirstOrDefault();
            if (firstPayment == null)
                return false;

            if (firstPayment.Item1.Card.Id != target.Card.Id)
                return false;

            if (firstPayment.Item2 != numberOfResources && !isVariableCost)
                return false;

            return true;
        }
    }
}
