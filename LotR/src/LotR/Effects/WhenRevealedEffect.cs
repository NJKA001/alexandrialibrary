using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class WhenRevealedEffect
        : CardEffectBase, IWhenRevealedEffect
    {
        protected WhenRevealedEffect(string description, ISource source)
            : base(description, source)
        {
        }
    }
}
