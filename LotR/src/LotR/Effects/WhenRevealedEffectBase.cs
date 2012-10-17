using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public abstract class WhenRevealedEffectBase
        : ForcedCardEffectBase, IWhenRevealedEffect
    {
        protected WhenRevealedEffectBase(string description, ISource source)
            : base(description, source)
        {
        }

        public virtual void WhenRevealed(IGame game)
        {
            game.AddEffect(this);
        }
    }
}
