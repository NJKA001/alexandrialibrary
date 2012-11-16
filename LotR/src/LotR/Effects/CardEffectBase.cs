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
        protected CardEffectBase(string type, string text, ICard cardSource)
            : base(type, text, cardSource)
        {
            this.cardSource = cardSource;
        }

        private readonly ICard cardSource;

        public ICard CardSource
        {
            get { return cardSource; }
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(type) ?
                string.Format("{0}: {1}", type, text)
                : text;
        }
    }
}
