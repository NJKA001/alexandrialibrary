using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class PassiveCardEffectBase
        : CardEffectBase, IPassiveEffect
    {
        protected PassiveCardEffectBase(string description, ISource source)
            : base(description, source)
        {
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
