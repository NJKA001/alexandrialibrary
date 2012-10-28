using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public abstract class SetupEffectBase
        : PassiveCardEffectBase, ISetupEffect
    {
        protected SetupEffectBase(string description, ISource source)
            : base(description, source)
        {
        }

        public abstract void Setup(IGame game);

        public override string ToString()
        {
            return string.Format("Setup: {0}", Description);
        }
    }
}
