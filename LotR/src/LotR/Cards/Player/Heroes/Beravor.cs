using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            private void PlayerDrawsTwoCards(IGame game, IEffectHandle handle, IPlayer controller, IPlayer player)
            {
                player.DrawCards(2);

                if (controller == player)
                {
                    handle.Resolve(string.Format("{0} chose to draw 2 cards", controller.Name));
                }
                else
                {
                    handle.Resolve(string.Format("{0} chose to have {1} draw 2 cards", controller.Name, player.Name));
                }
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var limit = new Limit(PlayerScope.Controller, TimeScope.Round, 1);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return new EffectHandle(this, null, null, limit);

                var builder =
                    new ChoiceBuilder("Choose a player to draw 2 cards", game, controller)
                        .Question("Which player should draw 2 cards?")
                            .LastAnswers(game.Players, item => item.Name, (source, handle, player) => PlayerDrawsTwoCards(game, handle, controller, player));

                var choice = builder.ToChoice();

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (exhaustable == null)
                    return new EffectHandle(this, choice, null, limit);

                var cost = new ExhaustSelf(exhaustable);

                return new EffectHandle(this, choice, cost, limit);
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
        }
    }
}
