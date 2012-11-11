using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Payments
{
    public interface IResourcePayment
        : IPayment
    {
        ICostlyCard CostlyCard { get; }
        ICardEffect CardEffect { get; }
        IEnumerable<ICharacterInPlay> Characters { get; }
        
        void AddPayment(ICharacterInPlay character, byte numberOfResources);
        void RemovePayment(Guid characterId);

        byte GetPaymentBy(Guid characterId);
        byte GetTotalPayment();
    }
}
