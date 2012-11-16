using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Effects.Phases.Any
{
    public class CardEntersPlayEffect
        : FrameworkEffectBase
    {
        public CardEntersPlayEffect(IGame game, ICardInPlay cardInPlay)
            : base("Card Enters Play", "When a card enters play", game)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            this.cardInPlay = cardInPlay;
        }

        private readonly ICardInPlay cardInPlay;

        public override void Resolve(IGame game, IEffectHandle handle)
        {
            var state = new CardEntersPlay(game, cardInPlay);

            foreach (var inPlay in game.GetCardsInPlayWithEffect<ICardInPlay, IAfterCardEntersPlay>())
            {
                foreach (var effect in inPlay.BaseCard.Text.Effects.OfType<IAfterCardEntersPlay>())
                {
                    effect.AfterCardEntersPlay(state);
                }
            }

            handle.Resolve(GetCompletedStatus());
        }
    }
}
