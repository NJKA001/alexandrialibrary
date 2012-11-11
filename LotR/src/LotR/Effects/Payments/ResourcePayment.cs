using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Payments
{
    public class ResourcePayment
        : PaymentBase, IResourcePayment
    {
        private ResourcePayment()
            : base("Resource Payment")
        {
        }

        public ResourcePayment(ICostlyCard costlyCard)
            : this()
        {
            this.costlyCard = costlyCard;
            this.cardEffect = null;
        }

        public ResourcePayment(ICardEffect cardEffect)
            : this()
        {
            this.costlyCard = null;
            this.cardEffect = cardEffect;
        }

        private readonly ICostlyCard costlyCard;
        private readonly ICardEffect cardEffect;
        private readonly List<ICharacterInPlay> characters = new List<ICharacterInPlay>();
        private readonly Dictionary<Guid, byte> payments = new Dictionary<Guid, byte>();

        public ICostlyCard CostlyCard
        {
            get { return costlyCard; }
        }

        public ICardEffect CardEffect
        {
            get { return cardEffect; }
        }

        public IEnumerable<ICharacterInPlay> Characters
        {
            get { return characters; }
        }

        public void AddPayment(ICharacterInPlay character, byte numberOfResources)
        {
            if (character == null)
                throw new ArgumentNullException("character");

            if (payments.ContainsKey(character.Card.Id))
                throw new ArgumentException("character is already included in this payment");

            if (character.Resources < numberOfResources)
                throw new ArgumentException("character does not have enough resources to make a payment of: " + numberOfResources);

            characters.Add(character);
            payments.Add(character.Card.Id, numberOfResources);
        }

        public void RemovePayment(Guid characterId)
        {
            if (!payments.ContainsKey(characterId))
                throw new ArgumentException("character does not have a payment to remove");

            payments.Remove(characterId);

            var character = characters.Where(x => x.Card.Id == characterId).FirstOrDefault();
            if (character == null)
                return;

            characters.Remove(character);
        }

        public byte GetPaymentBy(Guid characterId)
        {
            return (payments.ContainsKey(characterId)) ?
                payments[characterId]
                : (byte)0;
        }

        public byte GetTotalPayment()
        {
            var total = payments.Values.Sum(x => (decimal)x);
            return (total < 255) ?
                (byte)total
                : (byte)255;
        }
    }
}
