using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases.Any;

namespace LotR.Cards
{
    public abstract class CardBase
        : ICard, ISource
    {
        protected CardBase(string title, CardSet cardSet, uint cardNumber)
        {
            this.Title = title;
            this.CardSet = cardSet;
            this.CardNumber = cardNumber;
        }

        private readonly Guid id = Guid.NewGuid();
        private readonly CardText text = new CardText();
        private readonly List<Trait> traits = new List<Trait>();

        protected void AddTrait(Trait trait)
        {
            traits.Add(trait);
        }

        protected void AddEffect(ICardEffect effect)
        {
            text.AddEffect(effect);
        }

        protected string FlavorText
        {
            get { return text.FlavorText; }
            set { text.FlavorText = value; }
        }

        public Guid Id
        {
            get { return id; }
        }

        public string Title
        {
            get;
            private set;
        }

        public CardSet CardSet
        {
            get;
            private set;
        }

        public uint CardNumber
        {
            get;
            private set;
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

        public virtual bool HasTrait(Trait trait)
        {
            return traits.Any(x => x == trait);
        }

        public bool IsUnique
        {
            get;
            protected set;
        }
    }
}
