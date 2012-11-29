using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public interface IHand<T>
        where T : ICard
    {
        IEnumerable<T> Cards { get; }
        uint Size { get; }

        void AddCards(IEnumerable<T> cards);
        void RemoveCards(IEnumerable<T> cards);
        void RegisterCardAddedCallback(Action<T> callback);
        void RegisterCardRemovedCallback(Action<T> callback);
        
        T GetRandomCard();
    }
}
