using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
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

        public virtual IChoice GetChoice(IPhaseStep step)
        {
            return null;
        }

        public virtual ICost GetCost(IPhaseStep step)
        {
            return null;
        }

        public virtual ILimit GetLimit(IPhaseStep step)
        {
            return null;
        }

        public virtual void Setup(IPhaseStep step, IPayment payment)
        {
        }

        public virtual void Resolve(IPhaseStep step, IChoice choice)
        {
        }
    }
}
