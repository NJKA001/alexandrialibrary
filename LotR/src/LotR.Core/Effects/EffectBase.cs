using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects
{
    public abstract class EffectBase
        : IEffect
    {
        protected EffectBase(string description)
        {
            Description = description;
        }

        public string Description
        {
            get;
            private set;
        }

        public virtual ICost GetCost()
        {
            return null;
        }
    }
}
