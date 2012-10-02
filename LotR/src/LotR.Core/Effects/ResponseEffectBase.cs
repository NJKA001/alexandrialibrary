using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects
{
    public abstract class ResponseEffectBase
        : CardEffectBase, IResponse
    {
        protected ResponseEffectBase(string description, ICard source)
            : base(description, source)
        {
            this.Source = source;
        }

        public abstract void Resolve(IPhaseStep step, IPayment payment);
    }
}
