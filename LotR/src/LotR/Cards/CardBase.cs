using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases.Any;
using LotR.States.Phases.Any;

namespace LotR.Cards
{
    public abstract class CardBase
        : ICard, ISource
    {
        protected CardBase(CardType printedCardType, string title, CardSet cardSet, uint cardNumber)
        {
            this.PrintedCardType = printedCardType;
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

        protected string BacksideFlavorText
        {
            get { return text.BacksideFlavorText; }
            set { text.BacksideFlavorText = value; }
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

        public CardType PrintedCardType
        {
            get;
            private set;
        }

        public IEnumerable<Trait> PrintedTraits
        {
            get { return traits; }
        }

        public object Image
        {
            get;
            protected set;
        }

        public virtual void DuringCheckForResourceIcon(ICheckForResourceIcon state)
        {
            if (state.Target.Card.Id != this.id)
                return;

            foreach (var effect in text.Effects.OfType<IDuringCheckForResourceIcon>())
            {
                effect.DuringCheckForResourceIcon(state);
            }
        }

        public virtual void DuringCheckForTrait(ICheckForTrait state)
        {
            if (state.Target.StateId != this.id)
                return;

            if (traits.Any(x => x == state.Trait))
                state.HasTrait = true;

            foreach (var effect in text.Effects.OfType<IDuringCheckForTrait>())
            {
                effect.DuringCheckForTrait(state);
            }
        }

        public bool IsUnique
        {
            get;
            protected set;
        }

        public bool HasEffect<T>()
            where T : IEffect
        {
            return (Text.Effects.OfType<T>().Count() > 0);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("{0} ({1})\r\n", Title, PrintedCardType);
            sb.AppendFormat("Traits: {0}\r\n", string.Join(", ", PrintedTraits));
            foreach (var effect in Text.Effects)
            {
                sb.AppendLine(effect.ToString());
            }
            return sb.ToString();
        }
    }
}
