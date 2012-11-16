using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class ActionCardEffectBase
        : CardEffectBase, IActionEffect
    {
        protected ActionCardEffectBase(string text, ICard cardSource)
            : base("Action", text, cardSource)
        {
        }
    }
}
