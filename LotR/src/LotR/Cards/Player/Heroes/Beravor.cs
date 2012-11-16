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

            public override IEffectHandle GetHandle(IGame game)
            {
                var limit = new Limit(PlayerScope.Controller, TimeScope.Round, 1);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return new EffectHandle(null, null, limit);

                var choice = new ChoosePlayer(source, controller, game.Players.ToList());

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (exhaustable == null)
                    return new EffectHandle(choice, null, limit);

                var cost = new ExhaustSelf(exhaustable);

                return new EffectHandle(choice, cost, limit);
            }

            public override void Validate(IGame game, IEffectHandle handle)
            {
                var exhaustPayment = handle.Payment as IExhaustCardPayment;
                if (exhaustPayment == null)
                {
                    handle.Reject();
                    return;
                }

                if (exhaustPayment.Exhaustable.IsExhausted)
                {
                    handle.Reject();
                    return;
                }

                exhaustPayment.Exhaustable.Exhaust();

                handle.Accept();
            }

            public override void Resolve(IGame game, IEffectHandle handle)
            {
                var playerChoice = handle.Choice as IChoosePlayer;
                if (playerChoice == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var cards = playerChoice.ChosenPlayer.Deck.GetFromTop(2);
                playerChoice.ChosenPlayer.Hand.AddCards(cards);

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
