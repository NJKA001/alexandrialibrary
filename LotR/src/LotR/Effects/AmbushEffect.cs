using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Effects
{
    public class AmbushEffect
        : CardEffectBase, IAmbushEffect
    {
        public AmbushEffect(ICard cardSource)
            : base("Ambush", "After this enemy enters play, each player makes an engagement check against it", cardSource)
        {
        }

        public override string ToString()
        {
            return "Ambush (after this enemy enters play, each player makes an engagement check against it.)";
        }

        public void AfterCardEntersPlay(ICardEntersPlay state)
        {
            var enemy = state.EnteringPlay as IEnemyInPlay;
            if (enemy == null)
                return;

            //state.AddEffect(this);
        }
    }
}
