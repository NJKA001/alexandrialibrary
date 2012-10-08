using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class ActionEffectBase
        : CardEffectBase, IActionEffect
    {
        protected ActionEffectBase(string description, ICard source)
            : base(description, source)
        {
        }
    }
}
