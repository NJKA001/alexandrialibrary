using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public abstract class WhenRevealedEffectBase
        : PassiveCardEffectBase, IWhenRevealedEffect
    {
        protected WhenRevealedEffectBase(string description, ISource source)
            : base(description, source)
        {
        }

        public virtual void WhenRevealed(IGameState state)
        {
            state.AddEffect(this);
        }
    }
}
