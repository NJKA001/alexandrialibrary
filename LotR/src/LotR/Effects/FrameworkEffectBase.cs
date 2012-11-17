using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public abstract class FrameworkEffectBase
        : EffectBase, IFrameworkEffect
    {
        protected FrameworkEffectBase(string type, string text, IGame game)
            : base(type, text, game)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", type, text);
        }
    }
}
