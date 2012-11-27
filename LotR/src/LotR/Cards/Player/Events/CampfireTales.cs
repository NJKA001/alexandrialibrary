using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States;

namespace LotR.Cards.Player.Events
{
    public class CampfireTales
        : EventCardBase
    {
        public CampfireTales()
            : base("Campfire Tales", CardSet.SoM, 3, Sphere.Leadership, 1)
        {
            AddEffect(new EachPlayerDrawsOneCard(this));
        }

        private class EachPlayerDrawsOneCard
            : ActionCardEffectBase, IPlayerActionEffect
        {
            public EachPlayerDrawsOneCard(CampfireTales cardSource)
                : base("Each player draws 1 card.", cardSource)
            {
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                foreach (var player in game.Players)
                {
                    //TODO: Check for IDuringDrawingCards effects here

                    var drawEffect = new DrawingCardsEffect(game, player, 1);
                    var drawHandle = drawEffect.GetHandle(game);
                    game.AddEffect(drawEffect);
                    game.TriggerEffect(drawHandle);
                }

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
