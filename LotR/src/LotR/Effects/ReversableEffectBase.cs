using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects
{
    public abstract class ReversableEffectBase
        : ExecutableEffectBase, IReversableEffect
    {
        protected ReversableEffectBase(string description)
            : base(description)
        {
        }

        public abstract void Undo();
    }
}
