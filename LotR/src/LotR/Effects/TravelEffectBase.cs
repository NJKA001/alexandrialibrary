using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public abstract class TravelEffectBase
        : PassiveCardEffectBase, ITravelEffect
    {
        protected TravelEffectBase(string description, ISource source)
            : base(description, source)
        {
        }

        public virtual void Travel(IGameState state)
        {
            state.AddEffect(this);
        }
    }
}
