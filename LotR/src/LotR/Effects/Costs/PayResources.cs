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
        public PayResources(ISource source, Sphere resourceSphere, byte numberOfResources, bool isVariableCost)
            : base(GetDescription(resourceSphere, numberOfResources, isVariableCost), source)
        {
            this.resourceSphere = resourceSphere;
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

        private readonly Sphere resourceSphere;
        private readonly byte numberOfResources;
        private readonly bool isVariableCost;

        public Sphere ResourceSphere
        {
            get { return resourceSphere; }
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

            if (!isVariableCost && resourcePayment.GetTotalPayment() != numberOfResources)
                return false;

            if (resourcePayment.CostlyCard != null)
            {
                return resourcePayment.Characters.All(x => x.CanPayFor(resourcePayment.CostlyCard));
            }
            else if (resourcePayment.CardEffect != null)
            {
                return resourcePayment.Characters.All(x => x.CanPayFor(resourcePayment.CardEffect));
            }
            
            return true;
        }
    }
}
