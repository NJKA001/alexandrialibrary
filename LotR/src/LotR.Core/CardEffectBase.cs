using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class CardEffectBase
        : ICardEffect
    {
        protected CardEffectBase(ICard source, string description)
        {
            this.Source = source;
        }

        public ICard Source
        {
            get;
            protected set;
        }

        public string Description
        {
            get;
            protected set;
        }

        public virtual ICost GetCost()
        {
            return null;
        }
    }
}
