using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Effects.Phases.Any
{
    public class CardLeavesPlayEffect
        : FrameworkEffectBase
    {
        public CardLeavesPlayEffect(IGame game, ICardInPlay cardInPlay)
            : base("Card Leaves Play", "When a card leaves play", game)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            this.cardInPlay = cardInPlay;
        }

        private readonly ICardInPlay cardInPlay;

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
            var state = new CardLeavesPlay(game, cardInPlay);

            foreach (var inPlay in game.GetCardsInPlayWithEffect<ICardInPlay, IAfterCardEntersPlay>())
            {
                foreach (var effect in inPlay.BaseCard.Text.Effects.OfType<IAfterCardLeavesPlay>())
                {
                    effect.AfterCardLeavesPlay(state);
                }
            }
        }
    }
}
