using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Any;

namespace LotR.Core
{
    public abstract class CardBase
        : ICard
    {
        protected CardBase(string title, string setName, uint setNumber)
        {
            this.Title = title;
            this.SetName = setName;
            this.SetNumber = setNumber;
        }

        private readonly Guid id = Guid.NewGuid();
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

        public Guid Id
        {
            get { return id; }
        }

        public string Title
        {
            get;
            private set;
        }

        public string SetName
        {
            get;
            private set;
        }

        public uint SetNumber
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

        public virtual void CheckForTrait(ICheckForTraitStep step)
        {
            if (traits.Contains(step.Trait))
            {
                step.HasTrait = true;
            }
        }

        public bool IsUnique
        {
            get;
            protected set;
        }
    }
}
