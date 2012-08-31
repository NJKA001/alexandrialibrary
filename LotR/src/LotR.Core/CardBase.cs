using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class CardBase
        : ICard
    {
        private readonly List<Trait> traits = new List<Trait>();

        protected void AddTrait(Trait trait)
        {
            traits.Add(trait);
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
            get;
            protected set;
        }

        public object Image
        {
            get;
            protected set;
        }

        public bool HasTrait(Trait trait)
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
