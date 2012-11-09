using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Payments
{
    public class ResourcePayment
        : PaymentBase, IResourcePayment
    {
        public ResourcePayment(ICharacterInPlay character, byte numberOfResources)
            : this(new List<Tuple<ICharacterInPlay, byte>> { new Tuple<ICharacterInPlay, byte>(character, numberOfResources) })
        {
        }

        public ResourcePayment(IEnumerable<Tuple<ICharacterInPlay, byte>> payments)
            : base("Resource Payment")
        {
            if (payments == null)
                throw new ArgumentNullException("payments");

            this.Payments = payments;
        }

        public IEnumerable<Tuple<ICharacterInPlay, byte>> Payments
        {
            get;
            private set;
        }
    }
}
