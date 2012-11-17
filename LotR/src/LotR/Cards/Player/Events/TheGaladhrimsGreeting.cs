using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
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

            public override IEffectHandle GetHandle(IGame game)
            {
                return new EffectHandle(this, new ChooseGaladhrimsGreetingEffect(game, CardSource, playerCard.Owner));
            }

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
        }
    }
}
