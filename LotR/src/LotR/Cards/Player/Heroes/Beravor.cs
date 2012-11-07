using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects;
using LotR.Effects.Payments;
using LotR.States;
using LotR.Effects.Phases;

namespace LotR.Cards.Player.Heroes
{
    public class Beravor
        : HeroCardBase
    {
        public Beravor()
            : base("Beravor", CardSet.Core, 12, Sphere.Lore, 10, 2, 2, 2, 4)
        {
            AddTrait(Trait.Dunedain);
            AddTrait(Trait.Ranger);

            AddEffect(new ExhaustBeravorToDrawTwoCards(this));
        }

        private class ExhaustBeravorToDrawTwoCards
            : ActionCharacterAbilityBase
        {
            public ExhaustBeravorToDrawTwoCards(Beravor source)
                : base("Exhaust Beravor to choose a player. That player draws 2 cards. (Limit once per round)", source)
            {
            }

            public override IChoice GetChoice(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return null;

                return new ChoosePlayer(Source, controller);
            }

            public override ICost GetCost(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return null;

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                if (exhaustable == null)
                    return null;

                return new ExhaustSelf(exhaustable);
            }

            public override ILimit GetLimit(IGame game)
            {
                return new Limit(PlayerScope.Controller, TimeScope.Round, 1);
            }

            public override bool PaymentAccepted(IGame game, IPayment payment, IChoice choice)
            {
                var exhaustPayment = payment as IExhaustCardPayment;
                if (exhaustPayment == null)
                    return false;

                if (exhaustPayment.Exhaustable.IsExhausted)
                    return false;

                exhaustPayment.Exhaustable.Exhaust();
                
                return true;
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var playerChoice = choice as IChoosePlayer;
                if (playerChoice == null)
                    return;

                var cards = playerChoice.ChosenPlayer.Deck.GetFromTop(2);
                playerChoice.ChosenPlayer.Hand.AddCards(cards);
            }
        }
    }
}
