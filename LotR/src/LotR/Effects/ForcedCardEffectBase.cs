using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class ForcedCardEffectBase
        : PassiveCardEffectBase, IForcedEffect
    {
        protected ForcedCardEffectBase(string description, ICard cardSource)
            : base(description, cardSource)
        {
        }

        public override string ToString()
        {
            return string.Format("Forced: {0}", Description);
        }
    }
}
