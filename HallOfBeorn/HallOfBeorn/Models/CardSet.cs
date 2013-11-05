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
        }

        protected virtual void Initialize()
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
    }
}