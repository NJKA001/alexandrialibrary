using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;

namespace LotR.Effects
{
    public abstract class EffectBase
        : IEffect
    {
        protected EffectBase(string description)
        {
            EffectId = Guid.NewGuid();
            Description = description;
        }

        public Guid EffectId
        {
            get;
            private set;
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

        public virtual bool PaymentAccepted(IPhaseStep step, IPayment payment)
        {
            return true;
        }

        public virtual void Resolve(IPhaseStep step, IChoice choice)
        {
        }
    }
}
