using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Payments;

namespace LotR.Costs
{
    public class PayResourcesFrom
        : CostBase
    {
        public PayResourcesFrom(ICard source, IResourcefulCard target, byte numberOfResources)
            : base(GetDescription(target, numberOfResources), source)
        {
            this.target = target;
            this.numberOfResources = numberOfResources;
        }

        private static string GetDescription(IResourcefulCard target, byte numberOfResources)
        {
            return numberOfResources == 1 ?
                string.Format("Pay 1 resource from {0}'s pool", target.Title)
                : string.Format("Pay {0} resources from {1}'s pool", numberOfResources, target.Title);
        }

        private readonly IResourcefulCard target;
        private readonly byte numberOfResources;

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

            if (firstPayment.Item1.CardId != target.Id)
                return false;

            if (firstPayment.Item2 != numberOfResources)
                return false;

            return true;
        }
    }
}
