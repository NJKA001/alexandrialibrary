using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public class Deck<T>
        : IDeck<T>
        where T: ICard
    {
        public Deck()
        {
        }

        public Deck(IEnumerable<T> cards)
        {
            foreach (var card in cards)
            {
                this.cards.Add(card);
            }
        }

        private readonly IList<T> cards = new List<T>();
        private readonly IList<T> discardPile = new List<T>();

        public IEnumerable<T> Cards
        {
            get { return cards; }
        }

        public IEnumerable<T> DiscardPile
        {
            get { return discardPile; }
        }

        public uint Size
        {
            get { return (uint)cards.Count; }
        }

        public T GetFirst(Func<T, bool> predicate)
        {
            return cards.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetFromTop(uint numberOfCards)
        {
            if (numberOfCards == 0)
                throw new ArgumentException("numberOfCards must be greater than zero");

            return cards.Take((int)numberOfCards);
        }

        public void RemoveFromDeck(T card)
        {
            if (!this.cards.Contains(card))
                return;

            this.cards.Remove(card);
        }

        public void PutOnTop(IEnumerable<T> cards)
        {
            foreach (var card in cards)
            {
                this.cards.Add(card);
            }
        }

        public void PutOnBottom(IEnumerable<T> cards)
        {
            foreach (var card in cards)
            {
                this.cards.Insert(0, card);
            }
        }

        public void Draw(uint numberOfCards, Action<IEnumerable<T>> drawCallback)
        {
            if (numberOfCards == 0)
                throw new ArgumentException("numberOfCards must be greater than zero");

            var cardsToDraw = GetFromTop(numberOfCards).ToList();

            foreach (var card in cardsToDraw)
                RemoveFromDeck(card);

            if (drawCallback == null)
                return;

            drawCallback(cardsToDraw);
        }

        public void Discard(IEnumerable<T> cards)
        {
            foreach (var card in cards)
            {
                this.discardPile.Add(card);
            }
        }

        public void RemoveFromDiscardPile(IEnumerable<T> cards)
        {
            foreach (var card in cards)
            {
                if (this.discardPile.Contains(card))
                {
                    this.discardPile.Remove(card);
                }
            }
        }

        public void Shuffle()
        {            
            var cardArray = cards.ToArray();
            Shuffle(cardArray);
            Shuffle(cardArray);
            cards.Clear();

            foreach (var card in cardArray)
                cards.Add(card);
        }

        public void ShuffleIn(IEnumerable<T> cards)
        {
            foreach (var card in cards)
            {
                if (this.cards.Count < 2)
                {
                    PutOnBottom(new List<T> { card });
                    return;
                }
                else
                {
                    int index = random.Next(this.cards.Count - 1);
                    this.cards.Insert(index, card);
                }
            }
        }

        public void ShuffleDiscardPileIntoDeck()
        {
            if (discardPile.Count == 0)
                return;

            ShuffleIn(discardPile);
            discardPile.Clear();
        }

        private static readonly Random random = new Random();

        /// <summary>
        /// Shuffle the array.
        /// </summary>
        /// <typeparam name="T">Array element type.</typeparam>
        /// <param name="array">Array to shuffle.</param>
        private static void Shuffle<X>(X[] array)
        {
            for (int i = array.Length; i > 1; i--)
            {
                // Pick random element to swap.
                int j = random.Next(i); // 0 <= j <= i-1
                // Swap.
                X tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
        }
    }
}
