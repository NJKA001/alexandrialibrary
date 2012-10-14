using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class ForcedCardEffectBase
        : PassiveCardEffectBase, IForcedEffect
    {
        protected ForcedCardEffectBase(string description, ISource source)
            : base(description, source)
        {
        }
    }
}
