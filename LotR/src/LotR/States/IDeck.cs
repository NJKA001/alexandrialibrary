using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public interface IDeck<T>
        where T : ICard
    {
        IEnumerable<T> Cards { get; }
        IEnumerable<T> DiscardPile { get; }
        uint Size { get; }

        IEnumerable<T> GetFromTop(uint numberOfCards);
        void RemoveFromDeck(T card);
        void PutOnTop(IEnumerable<T> cards);
        void PutOnBottom(IEnumerable<T> cards);
        void Draw(uint numberOfCards, Action<IEnumerable<T>> drawCallback);
        void Discard(IEnumerable<T> cards);
        void RemoveFromDiscardPile(IEnumerable<T> cards);
        void Order<TKey>(Func<T, TKey> keySelector);
        void Shuffle();
        void ShuffleIn(IEnumerable<T> cards);
        void ShuffleDiscardPileIntoDeck();
    }
}
