using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class CardBase
        : ICard
    {
        private readonly CardText text = new CardText();
        private readonly List<Traits> traits = new List<Traits>();

        protected void Trait(Traits trait)
        {
            traits.Add(trait);
        }

        protected void Effect(ICardEffect effect)
        {
            text.AddEffect(effect);
        }

        public string Title
        {
            get;
            protected set;
        }

        public string SetName
        {
            get;
            protected set;
        }

        public uint SetNumber
        {
            get;
            protected set;
        }

        public ICardText Text
        {
            get { return text; }
        }

        public object Image
        {
            get;
            protected set;
        }

        public bool HasTrait(Traits trait)
        {
            return traits.Contains(trait);
        }

        public bool IsUnique
        {
            get;
            protected set;
        }
    }
}
