using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class DealDamageEffect
        : FrameworkEffectBase
    {
        public DealDamageEffect(IGame game, ICardInPlay cardInPlay, byte damage)
            : base("Deal Damage", "Deal " + damage + " damage to " + cardInPlay.Title, game)
        {
            this.cardInPlay = cardInPlay;
            this.damage = damage;
        }

        private readonly ICardInPlay cardInPlay;
        private readonly byte damage;

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
        }
    }
}
