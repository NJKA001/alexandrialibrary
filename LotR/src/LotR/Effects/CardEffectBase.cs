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
        protected CardEffectBase(string description, ISource source)
            : base(description)
        {
            Source = source;
        }

        public ISource Source
        {
            get;
            private set;
        }
    }
}
