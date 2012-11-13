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
            : base("Passive Card Effect", description, cardSource)
        {
        }

        protected PassiveCardEffectBase(string name, string description, ICard cardSource)
            : base(name, description, cardSource)
        {
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
