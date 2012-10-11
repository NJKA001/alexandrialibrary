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
        IEnumerable<T> DiscardedCards { get; }
        uint Size { get; }

        IEnumerable<T> GetFromTop(int numberOfCards);
        void PutOnTop(IEnumerable<T> cards);
        void PutOnBottom(IEnumerable<T> cards);
        void Shuffle();
    }
}
