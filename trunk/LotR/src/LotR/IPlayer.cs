﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IPlayer
        : INotifyPropertyChanged
    {
        byte StartingThreat { get; }
        IEnumerable<IHeroCard> StartingHeroes { get; }

        byte CurrentThreat { get; }
        IEnumerable<IHeroInPlay> HeroesInPlay { get; }
        IEnumerable<ICardInPlay> CardsInPlay { get; }
        IEnumerable<IPlayerCard> CardsInHand { get; }

        IEnumerable<IEnemyInPlay> EngagedEnemies { get; }

        IDeck<IPlayerCard> Deck { get; }
        IDeck<IPlayerCard> DiscardPile { get; }

        bool IsFirstPlayer { get; }

        void IncreaseThreat(byte value);
        void DecreaseThreat(byte value);
    }
}