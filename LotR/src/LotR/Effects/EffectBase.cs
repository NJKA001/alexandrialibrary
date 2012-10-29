using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.States;

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

        public virtual IChoice GetChoice(IGame game)
        {
            return null;
        }

        public virtual ICost GetCost(IGame game)
        {
            return null;
        }

        public virtual ILimit GetLimit(IGame game)
        {
            return null;
        }

        public virtual bool PaymentAccepted(IGame game, IPayment payment, IChoice choice)
        {
            return true;
        }

        public virtual void Resolve(IGame game, IPayment payment, IChoice choice)
        {
        }

        public virtual string GetResolutionDescription(IGame game, IPayment payment, IChoice choice)
        {
            return ToString();
        }
    }
}
