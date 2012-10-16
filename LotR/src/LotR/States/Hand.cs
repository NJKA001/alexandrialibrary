using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public class Hand<T>
        : StateBase, IHand<T>
        where T : ICard
    {
        public Hand()
        {
        }

        public Hand(IEnumerable<T> cards)
        {
            foreach (var card in cards)
            {
                this.cards.Add(card);
            }
        }

        private readonly IList<T> cards = new List<T>();
        private readonly Random random = new Random();

        public IEnumerable<T> Cards
        {
            get { return cards; }
        }

        public uint Size
        {
            get { return (uint)cards.Count; }
        }

        public void AddCards(IEnumerable<T> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");

            foreach (var card in cards)
            {
                this.cards.Add(card);
            }
        }

        public void RemoveCards(IEnumerable<T> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");

            foreach (var card in cards)
            {
                if (!this.cards.Contains(card))
                    continue;

                this.cards.Remove(card);
            }
        }

        public T GetRandomCard()
        {
            var index = random.Next(0, this.cards.Count);
            return cards[index];
        }
    }
}
