using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public abstract class FrameworkEffectBase
        : PassiveEffectBase, IFrameworkEffect
    {
        protected FrameworkEffectBase(string name, string description, IGame game)
            : base(name, description, game)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Description);
        }
    }
}
