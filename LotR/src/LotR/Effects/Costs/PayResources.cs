using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Costs
{
    public class PayResources
        : CostBase, IPayResources
    {
        public PayResources(ISource source, Sphere sphere, byte numberOfResources, bool isVariableCost)
            : base(GetDescription(sphere, numberOfResources, isVariableCost), source)
        {
            this.sphere = sphere;
            this.numberOfResources = numberOfResources;
            this.isVariableCost = isVariableCost;
        }

        private static string GetDescription(Sphere sphere, byte numberOfResources, bool isVariableCost)
        {
            if (isVariableCost)
            {
                return (sphere == Sphere.Neutral) ? "Pay any number of resources" : string.Format("Pay any number of {0} resources", sphere);
            }
            else
            {
                if (numberOfResources == 0)
                {
                    return (sphere == Sphere.Neutral) ? "No resource cost" : string.Format("No {0} resource cost", sphere);
                }
                else if (numberOfResources == 1)
                {
                    return (sphere == Sphere.Neutral) ? "Pay 1 resource" : string.Format("Pay 1 {0} resource", sphere);
                }
                else
                {
                    return (sphere == Sphere.Neutral) ? string.Format("Pay {0} resources", numberOfResources) : string.Format("Pay {0} {1} resources", numberOfResources, sphere);
                }
            }
        }

        private readonly Sphere sphere;
        private readonly byte numberOfResources;
        private readonly bool isVariableCost;

        public Sphere Sphere
        {
            get { return sphere; }
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

            var sum = resourcePayment.Payments.Sum(x => x.Item2);
            if (sum != numberOfResources)
                return false;

            if (sphere != Sphere.Neutral)
            {
                foreach (var source in resourcePayment.Payments.Select(x => x.Item1))
                {
                    //if (!source.CanPayFor(
                    //if (!source.HasResourceIcon(sphere))
                    //    return false;
                }
            }

            return true;
        }
    }
}
