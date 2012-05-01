using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IPlayer
        : INotifyPropertyChanged
    {
        ICard House { get; }
        IEnumerable<ICard> Agendas { get; }
        IEnumerable<Title> CurrentTitles { get; }

        byte TotalGold { get; }
        byte TotalPower { get; }

        IHand Hand { get; }
        IDeck Deck { get; }
        IPile DiscardPile { get; }
        IPile DeadPile { get; }
        IPile RemovedFromGamePile { get; }

        ICard DrawCardFromDeck();
        void DiscardTopCardFromDeck();

        void AddTitle(Title title);
        void RemoveTitle(Title title);
    }
}
