using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;

namespace LotR.Games
{
    public interface IPlayer
        : INotifyPropertyChanged
    {
        string Name { get; }
        byte StartingThreat { get; }
        IEnumerable<IHeroCard> StartingHeroes { get; }

        byte CurrentThreat { get; }
        IEnumerable<IHeroInPlay> HeroesInPlay { get; }
        IEnumerable<ICardInPlay> CardsInPlay { get; }
        IEnumerable<IPlayerCard> CardsInHand { get; }

        IEnumerable<IEnemyInPlay> EngagedEnemies { get; }

        IPlayerDeck Deck { get; }
        IDeck<IPlayerCard> DiscardPile { get; }

        bool IsFirstPlayer { get; }

        void IncreaseThreat(byte value);
        void DecreaseThreat(byte value);
        void AddCardsToHand(IEnumerable<IPlayerCard> cards);
        void RemoveCardsFromHand(IEnumerable<IPlayerCard> cards);
        void DrawCards(byte value);
    }
}
