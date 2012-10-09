using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects.Payments;

namespace LotR.Effects.Costs
{
    public class PayResources
        : CostBase
    {
        public PayResources(ISource source, Sphere sphere, byte numberOfResources)
            : base(string.Format("Pay {0} {1} resources", numberOfResources, sphere), source)
        {
            this.sphere = sphere;
            this.numberOfResources = numberOfResources;
        }

        private readonly Sphere sphere;
        private readonly byte numberOfResources;

        public override bool IsMetBy(IPayment payment)
        {
            if (payment == null)
                return false;

            var resourcePayment = payment as IResourcePayment;
            if (resourcePayment == null)
                return false;

            var sum = resourcePayment.Payments.Sum(x => x.Item2);
            if (sum != numberOfResources)
                return false;

            if (sphere != Sphere.Neutral)
            {
                foreach (var source in resourcePayment.Payments.Select(x => x.Item1))
                {
                    if (!source.HasResourceIcon(sphere))
                        return false;
                }
            }

            return true;
        }
    }
}
