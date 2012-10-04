using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class ExecutableEffectBase
        : EffectBase, IExecutableEffect
    {
        protected ExecutableEffectBase(string description)
            : base(description)
        {
        }

        public abstract void Execute();
    }
}
