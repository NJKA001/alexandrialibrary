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
        protected CardEffectBase(EffectType type, string description, ICard cardSource)
            : base(type, description, cardSource)
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
            return (Type != EffectType.Passive) ?
                string.Format("{0}: {1}", Type.ToString().Replace('_', ' '), Text)
                : Text;
        }
    }
}
