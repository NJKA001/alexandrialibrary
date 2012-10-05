using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IDeck<T>
        where T : ICard
    {
        IEnumerable<T> Cards { get; }
        uint Size { get; }

        T GetFirst(Func<T, bool> predicate);
        IEnumerable<T> GetFromTop(int numberOfCards);
        void PutOnTop(IEnumerable<T> cards);
        void PutOnBottom(IEnumerable<T> cards);
        void Shuffle();
    }
}
