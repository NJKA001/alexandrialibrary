using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.Cards
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

        public string BacksideFlavorText
        {
            get;
            set;
        }

        public void AddEffect(ICardEffect effect)
        {
            effects.Add(effect);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var effect in effects)
            {
                if (sb.Length > 0)
                {
                    sb.Append(Environment.NewLine);
                }

                sb.AppendLine(effect.ToString());
            }

            return sb.ToString();
        }
    }
}
