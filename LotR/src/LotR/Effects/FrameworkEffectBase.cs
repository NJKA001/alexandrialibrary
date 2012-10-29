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
        protected FrameworkEffectBase(string description, IGame game)
            : base(description, game)
        {
        }
    }
}
