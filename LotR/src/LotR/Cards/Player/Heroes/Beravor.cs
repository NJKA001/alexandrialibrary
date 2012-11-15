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

            public override IEffectOptions GetOptions(IGame game)
            {
                var limit = new Limit(PlayerScope.Controller, TimeScope.Round, 1);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return new EffectOptions(null, null, limit);

                var choice = new ChoosePlayer(Source, controller, game.Players.ToList());

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                if (exhaustable == null)
                    return new EffectOptions(choice, null, limit);

                var cost = new ExhaustSelf(exhaustable);

                return new EffectOptions(choice, cost, limit);
            }

            public override bool PaymentAccepted(IGame game, IEffectOptions options)
            {
                var exhaustPayment = options.Payment as IExhaustCardPayment;
                if (exhaustPayment == null)
                    return false;

                if (exhaustPayment.Exhaustable.IsExhausted)
                    return false;

                exhaustPayment.Exhaustable.Exhaust();
                
                return true;
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var playerChoice = options.Choice as IChoosePlayer;
                if (playerChoice == null)
                    return GetCancelledString();

                var cards = playerChoice.ChosenPlayer.Deck.GetFromTop(2);
                playerChoice.ChosenPlayer.Hand.AddCards(cards);

                return ToString();
            }
        }
    }
}
