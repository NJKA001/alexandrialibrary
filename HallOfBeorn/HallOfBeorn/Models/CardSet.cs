using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class CardSet
    {
        public CardSet()
        {
            Cards = new List<Card>();

            Initialize();

            foreach (var card in Cards)
            {
                card.CardSet = this;
            }
        }

        protected virtual void Initialize()
        {
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public List<Card> Cards { get; protected set; }
    }
}