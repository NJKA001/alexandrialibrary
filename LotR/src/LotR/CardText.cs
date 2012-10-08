using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public class CardText
        : ICardText
    {
        public CardText()
        {
        }

        public CardText(IEnumerable<ICardEffect> effects)
        {
            this.effects = effects.ToList();
        }

        private readonly IList<ICardEffect> effects = new List<ICardEffect>();

        public IEnumerable<ICardEffect> Effects
        {
            get { return effects; }
        }

        public string FlavorText
        {
            get;
            set;
        }

        public void AddEffect(ICardEffect effect)
        {
            effects.Add(effect);
        }
    }
}
