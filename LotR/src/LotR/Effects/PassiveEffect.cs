using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class PassiveEffect
        : CardEffectBase, IPassiveEffect
    {
        protected PassiveEffect(string description, ICard source)
            : base(description, source)
        {
        }
    }
}
