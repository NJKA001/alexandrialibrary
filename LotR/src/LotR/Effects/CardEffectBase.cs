using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class CardEffectBase
        : EffectBase, ICardEffect
    {
        protected CardEffectBase(string description, ICard cardSource)
            : base(description, cardSource)
        {
            CardSource = cardSource;
        }

        public ICard CardSource
        {
            get;
            private set;
        }
    }
}
