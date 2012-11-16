using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Allies;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Cards.Player.Events
{
    public class SneakAttack
        : EventCardBase
    {
        public SneakAttack()
            : base("Sneak Attack", CardSet.Core, 23, Sphere.Leadership, 1)
        {
            AddEffect(new PutOneAllyIntoPlay(this));
        }

        private class PutOneAllyIntoPlay
            : ActionCardEffectBase, IPlayerActionEffect
        {
            public PutOneAllyIntoPlay(SneakAttack source)
                : base("Put 1 ally card into play from your hand. At the end of the phase, if that ally is still in play, return it to your hand.", source)
            {
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var playerCard = CardSource as IPlayerCard;
                if (playerCard == null)
                    throw new InvalidOperationException();

                var owner = playerCard.Owner;
                if (owner == null)
                    throw new InvalidOperationException();

                var alliesInHand = owner.Hand.Cards.OfType<IAllyCard>().ToList();

                return new EffectOptions(new ChooseCardInHand<IAllyCard>("Choose 1 ally card in your hand", source, owner, alliesInHand));
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var chooseAlly = options.Choice as IChooseCardInHand<IAllyCard>;
                if (chooseAlly == null || chooseAlly.ChosenCard == null)
                    return GetCancelledString();

                var playerCard = CardSource as IPlayerCard;
                if (playerCard == null)
                    throw new InvalidOperationException();

                var owner = playerCard.Owner;
                if (owner == null)
                    throw new InvalidOperationException();

                owner.Hand.RemoveCards(new List<IPlayerCard> { chooseAlly.ChosenCard });
                owner.AddCardInPlay(new AllyInPlay(game, chooseAlly.ChosenCard));
                game.AddEffect(new AtEndOfPhaseReturnAllyToYourHand(CardSource, chooseAlly.ChosenCard.Id));

                return ToString();
            }
        }

        private class AtEndOfPhaseReturnAllyToYourHand
            : PassiveCardEffectBase, IUntilEndOfPhase
        {
            public AtEndOfPhaseReturnAllyToYourHand(ICard cardSource, Guid allyId)
                : base("At the end of the phase, if that ally is still in play, return it to your hand.", cardSource)
            {
                this.allyId = allyId;
            }

            public override bool CanBeTriggered(IGame game)
            {
                return IsEndOfPhase(game);
            }

            private readonly Guid allyId;

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var allyInPlay = game.GetCardInPlay<IAllyInPlay>(allyId);
                if (allyInPlay == null)
                    return GetCancelledString();

                var allyController = game.GetController(allyId);
                if (allyController == null)
                    return GetCancelledString();

                allyController.RemoveCardInPlay(allyInPlay);

                var eventController = game.GetController(CardSource.Id);
                if (eventController == null)
                    return GetCancelledString();

                eventController.Hand.AddCards(new List<IPlayerCard> { allyInPlay.Card });

                return ToString();
            }
        }
    }
}
