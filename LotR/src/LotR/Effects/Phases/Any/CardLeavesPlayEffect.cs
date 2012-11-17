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

        public override void Trigger(IGame game, IEffectHandle handle)
        {
            if (game.StagingArea.CardsInStagingArea.Any(x => x.Card.Id == cardInPlay.BaseCard.Id))
            {
                game.StagingArea.RemoveFromStagingArea(cardInPlay as IEncounterInPlay);
            }
            else if (game.QuestArea.ActiveLocation != null && game.QuestArea.ActiveLocation.Card.Id == cardInPlay.BaseCard.Id)
            {
                game.QuestArea.RemoveActiveLocation();
            }
            else
            {
                foreach (var player in game.Players)
                {
                    if (player.CardsInPlay.Any(x => x.BaseCard.Id == cardInPlay.BaseCard.Id))
                    {
                        player.RemoveCardInPlay(cardInPlay);
                        break;
                    }
                }
            }

            var state = new CardLeavesPlay(game, cardInPlay);

            foreach (var card in game.GetCardsInPlayWithEffect<ICardInPlay, IAfterCardLeavesPlay>())
            {
                foreach (var effect in card.BaseCard.Text.Effects.OfType<IAfterCardLeavesPlay>())
                {
                    effect.AfterCardLeavesPlay(state);
                }
            }

            handle.Resolve(GetCompletedStatus());
        }
    }
}
