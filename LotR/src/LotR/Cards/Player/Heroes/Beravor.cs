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

            private void ExhaustAndPlayerDrawsTwoCards(IGame game, IEffectHandle handle, IPlayer controller, IExhaustableInPlay exhaustable, IPlayer player)
            {
                exhaustable.Exhaust();

                player.DrawCards(2);

                if (controller.StateId == player.StateId)
                {
                    handle.Resolve(string.Format("{0} exhausted '{1}' to draw 2 cards", controller.Name, CardSource.Title));
                }
                else
                {
                    handle.Resolve(string.Format("{0} exhausted '{1}' to have {2} draw 2 cards", controller.Name, CardSource.Title, player.Name));
                }
            }

            private void CancelEffect(IGame game, IEffectHandle handle, IPlayer controller)
            {
                handle.Cancel(string.Format("{0} chose not to exhaust '{0}' to have a player draw 2 cards", controller.Name, CardSource.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var limit = new Limit(PlayerScope.Controller, TimeScope.Round, 1);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (exhaustable == null || exhaustable.IsExhausted)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder(string.Format("Exhaust '{0}' to have a plyer draw 2 cards", CardSource.Title), game, controller);
                
                if (game.Players.Count() == 1)
                {
                    builder.Question(string.Format("You are the only player, exhaust '{0}' to draw 2 cards?", CardSource.Title))
                        .Answer(string.Format("Yes, I want to exhaust '{0}' to draw 2 cards", CardSource.Title), controller, (source, handle, player) => ExhaustAndPlayerDrawsTwoCards(source, handle, controller, exhaustable, player))
                        .LastAnswer("No, I do not want to exhaust '{0}' to draw 2 cards", false, (source, handle, item) => CancelEffect(source, handle, controller));
                }
                else
                {
                    builder.Question(string.Format("{0}, do you want to exhaust '{1}' to have a player draw 2 cards?", controller.Name))
                        .Answer(string.Format("Yes, I will exhaust '{0}' to have a player draw 2 cards", CardSource.Title), true)
                            .Question("Which player should draw 2 cards?")
                                .LastAnswers(game.Players.ToList(), item => item.Name, (source, handle, player) => ExhaustAndPlayerDrawsTwoCards(game, handle, controller, exhaustable, player))
                        .LastAnswer(string.Format("No, I do not want to exhaust '{0}' to have a player draw 2 cards", CardSource.Title), false, (source, handle, item) => CancelEffect(source, handle, controller));
                        
                }

                var choice = builder.ToChoice();

                return new EffectHandle(this, choice, limit);
            }
        }
    }
}
