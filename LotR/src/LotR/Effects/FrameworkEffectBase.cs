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
        protected FrameworkEffectBase(string name, string text, IGame game)
            : base(EffectType.Framework, text, game)
        {
            this.name = name;
        }

        protected readonly string name;

        public override string ToString()
        {
            return string.Format("{0}: {1}", name, Text);
        }
    }
}
