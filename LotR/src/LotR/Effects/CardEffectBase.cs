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
        protected CardEffectBase(string name, string description, ICard cardSource)
            : base(name, description, cardSource)
        {
            this.cardSource = cardSource;
            this.name = name;
        }

        private readonly ICard cardSource;
        private readonly string name;

        public ICard CardSource
        {
            get { return cardSource; }
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Name) ?
                string.Format("{0}: {1}", Name, Description)
                : Description;
        }
    }
}
