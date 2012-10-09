using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class ActionEffectBase
        : CardEffectBase, IActionEffect
    {
        protected ActionEffectBase(string description, ISource source)
            : base(description, source)
        {
        }
    }
}
