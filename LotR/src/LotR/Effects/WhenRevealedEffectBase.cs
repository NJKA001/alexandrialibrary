using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects
{
    public abstract class WhenRevealedEffectBase
        : ForcedCardEffectBase, IWhenRevealedEffect
    {
        protected WhenRevealedEffectBase(string description, ICard cardSource)
            : base(description, cardSource)
        {
        }

        public virtual void WhenRevealed(IGame game)
        {
            game.AddEffect(this);
        }

        public override string ToString()
        {
            return string.Format("When Revealed: {0}", text);
        }
    }
}
