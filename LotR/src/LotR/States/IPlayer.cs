using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;

namespace LotR.States
{
    public interface IPlayer
        : IState
    {
        string Name { get; }
        IPlayerDeck Deck { get; }
        IEnumerable<IAttachableInPlay> DeckAttachments { get; }
        IHand<IPlayerCard> Hand { get; }
        IEnumerable<ICardInPlay> CardsInPlay { get; }
        IEnumerable<IEnemyInPlay> EngagedEnemies { get; }

        byte CurrentThreat { get; }
        bool IsFirstPlayer { get; set; }
        bool IsActivePlayer { get; set; }

        void IncreaseThreat(byte value);
        void DecreaseThreat(byte value);
        void DiscardFromHand(IEnumerable<IPlayerCard> cards);

        void AddDeckAttachment(IAttachableInPlay attachment);
        void RemoveDeckAttachment(IAttachableInPlay attachment);

        void AddCardInPlay(ICardInPlay card);
        void RemoveCardInPlay(ICardInPlay card);

        void AddEngagedEnemy(IEnemyInPlay enemy);
        void RemoveEngagedEnemy(IEnemyInPlay enemy);

        void DrawCards(uint numberOfCards);

        void RegisterCardAddedToPlayCallback(Action<ICardInPlay> callback);
        void RegisterCardRemovedFromPlayCallback(Action<ICardInPlay> callback);

        bool IsTheControllerOf(ICardInPlay card);
    }
}
