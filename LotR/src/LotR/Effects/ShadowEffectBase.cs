using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public abstract class ShadowEffectBase
        : PassiveCardEffectBase, IShadowEffect
    {
        protected ShadowEffectBase(string description, ISource source)
            : base(description, source)
        {
        }

        public virtual void Shadow(IGame game)
        {
            game.AddEffect(this);
        }
    }
}
