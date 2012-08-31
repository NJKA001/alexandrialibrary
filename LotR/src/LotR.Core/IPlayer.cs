using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IPlayer
        : INotifyPropertyChanged
    {
        byte StartingThreat { get; }
        IEnumerable<IHeroCard> StartingHeroes { get; }

        byte CurrentThreat { get; }
        IEnumerable<IHeroInPlay> HeroesInPlay { get; }

        IDeck<IPlayerCard> Deck { get; }
        IEnumerable<ICardInPlay> CardsInPlay { get; }
        IEnumerable<IPlayerCard> CardsInHand { get; }

        void IncreaseThreat(byte value);
        void DecreaseThreat(byte value);
    }
}
