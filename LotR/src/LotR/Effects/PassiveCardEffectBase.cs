using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class PassiveCardEffectBase
        : CardEffectBase, IPassiveEffect
    {
        protected PassiveCardEffectBase(string description, ICard cardSource)
            : base(description, cardSource)
        {
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
