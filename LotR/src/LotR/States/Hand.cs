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
        public Hand(IGame gameState)
            : base(gameState)
        {
        }

        public Hand(IGame gameState, IEnumerable<T> cards)
            : base(gameState)
        {
            foreach (var card in cards)
            {
                this.cards.Add(card);
            }
        }

        private readonly IList<T> cards = new List<T>();
        private readonly Random random = new Random();
        private readonly IList<Action<T>> cardAddedCallbacks = new List<Action<T>>();
        private readonly IList<Action<T>> cardRemovedCallbacks = new List<Action<T>>();

        private void AddCard(T card)
        {
            if (cards.Contains(card))
                throw new InvalidOperationException("Cannot add card - card is already contained in hand: " + card.Title);

            cards.Add(card);

            foreach (var callback in cardAddedCallbacks)
                callback(card);
        }

        private void RemoveCard(T card)
        {
            if (!cards.Contains(card))
                throw new InvalidOperationException("Cannot remove card - card is not contained in hand: " + card.Title);

            cards.Remove(card);

            foreach (var callback in cardRemovedCallbacks)
                callback(card);
        }

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
                AddCard(card);
            }
        }

        public void RemoveCards(IEnumerable<T> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");

            foreach (var card in cards)
            {
                RemoveCard(card);
            }
        }

        public void RegisterCardAddedCallback(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            cardAddedCallbacks.Add(callback);
        }

        public void RegisterCardRemovedCallback(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            cardRemovedCallbacks.Add(callback);
        }

        public T GetRandomCard()
        {
            var index = random.Next(0, this.cards.Count);
            return cards[index];
        }
    }
}
