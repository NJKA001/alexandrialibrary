using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public abstract class CardBase
        : ICard
    {
        protected CardBase(string title, CardType type, CardSet set)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            
            this.title = title;
            this.type = type;
            this.set = set;
        }

        private readonly string title;
        private readonly CardType type;
        private readonly CardSet set;
        protected readonly ITextBuilder text;

        public string Title
        {
            get { return title; }
        }

        public CardType Type
        {
            get { return type; }
        }

        public CardSet Set
        {
            get { return set; }
        }

        public IText Text
        {
            get { return text.ToText(); }
        }
    }
}
