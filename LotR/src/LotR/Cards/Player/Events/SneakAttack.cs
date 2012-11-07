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

            public override IChoice GetChoice(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return null;

                var alliesInHand = controller.Hand.Cards.OfType<IAllyCard>().ToList();

                return new ChooseCardInHand<IAllyCard>("Choose 1 ally card in your hand", Source, controller, alliesInHand);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var chooseAlly = choice as IChooseCardInHand<IAllyCard>;
                if (chooseAlly == null || chooseAlly.ChosenCard == null)
                    return;

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return;

                controller.AddCardInPlay(new AllyInPlay(game, chooseAlly.ChosenCard));
                game.AddEffect(new AtEndOfPhaseReturnAllyToYourHand(CardSource, chooseAlly.ChosenCard.Id));
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
                return true;
            }

            private readonly Guid allyId;

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var allyInPlay = game.GetCardInPlay<IAllyInPlay>(allyId);
                if (allyInPlay == null)
                    return;

                var allyController = game.GetController(allyId);
                if (allyController == null)
                    return;

                allyController.RemoveCardInPlay(allyInPlay);

                var eventController = game.GetController(CardSource.Id);
                if (eventController == null)
                    return;

                eventController.Hand.AddCards(new List<IPlayerCard> { allyInPlay.Card });
            }
        }
    }
}
