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

        private string normalizedName;

        protected virtual void Initialize()
        {
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        
        public string NormalizedName
        {
            get
            {
                return (!string.IsNullOrEmpty(normalizedName)) ?
                    normalizedName
                    : Name;
            }
            protected set
            {
                normalizedName = value;
            }
        }

        public string Abbreviation { get; protected set; }
        public string Cycle { get; protected set; }
        public int Number { get; protected set; }
        public SetType SetType { get; protected set; }
        public List<Card> Cards { get; protected set; }
    }
}