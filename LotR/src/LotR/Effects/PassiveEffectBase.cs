using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class PassiveEffectBase
        : CardEffectBase, IPassiveEffect
    {
        protected PassiveEffectBase(string description, ISource source)
            : base(description, source)
        {
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
