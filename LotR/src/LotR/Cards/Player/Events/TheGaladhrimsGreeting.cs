using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Cards.Player.Events
{
    public class TheGaladhrimsGreeting
        : EventCardBase
    {
        public TheGaladhrimsGreeting()
            : base("The Galadhrim's Greeting", CardSet.Core, 46, Sphere.Spirit, 3)
        {
            AddEffect(new LowerThreat(this));
        }

        private class LowerThreat
            : ActionCardEffectBase, IPlayerActionEffect
        {
            public LowerThreat(TheGaladhrimsGreeting cardSource)
                : base("Reduce 1 player's threat by 6, or reduce each player's threat by 2.", cardSource)
            {
                this.playerCard = cardSource;
            }

            private IPlayerCard playerCard;

            private void ReduceOnePlayersThreatBySix(IGame game, IEffectHandle handle, IPlayer player)
            {
                player.DecreaseThreat(6);
                handle.Resolve(string.Format("{0} had their threat reduced by 6", player.Name));
            }

            private void ReduceEachPlayersThreatByTwo(IGame game, IEffectHandle handle)
            {
                foreach (var player in game.Players)
                {
                    player.DecreaseThreat(2);
                }
                handle.Resolve("Each players had their threat reduced by 2");
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var builder =
                    new ChoiceBuilder("Reduce 1 player's threat by 6, or reduce each player's threat by 2.", game, playerCard.Owner)
                        .Question("Which effect would you like to trigger?")
                            .Answer("Reduce 1 player's threat by 6", 1)
                                .Question("Which players threat do you want to reduce by 6?")
                                    .LastAnswers(game.Players, (item) => item.Name, (source, handle, player) => ReduceOnePlayersThreatBySix(source, handle, player))
                            .LastAnswer("Reduce each player's threat by 2", 2, (source, handle, player) => ReduceEachPlayersThreatByTwo(source, handle));

                return new EffectHandle(this, builder.ToChoice()); //new ChooseGaladhrimsGreetingEffect(game, CardSource, playerCard.Owner));
            }

            /*
            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var effectChoice = handle.Choice as IChooseGaladhrimsGreetingEffect;
                if (effectChoice == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                if (effectChoice.ReduceOnePlayersThreatBySix != null)
                {
                    effectChoice.ReduceOnePlayersThreatBySix.DecreaseThreat(6);
                    handle.Resolve(string.Format("{0}'s threat has been reduced by 6", effectChoice.ReduceOnePlayersThreatBySix.Name));
                    return;
                }
                else if (effectChoice.ReduceEachPlayersThreatByTwo)
                {
                    foreach (var player in game.Players)
                    {
                        player.DecreaseThreat(2);
                    }
                    handle.Resolve("Each players threat has been reduced by 2");
                    return;
                }

                handle.Cancel(GetCancelledString());
            }
            */
        }
    }
}
