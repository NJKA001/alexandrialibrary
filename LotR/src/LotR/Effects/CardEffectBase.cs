using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class CardEffectBase
        : EffectBase, ICardEffect
    {
        protected CardEffectBase(string description, ICard source)
            : base(description)
        {
            Source = source;
        }

        public ICard Source
        {
            get;
            protected set;
        }
    }
}
