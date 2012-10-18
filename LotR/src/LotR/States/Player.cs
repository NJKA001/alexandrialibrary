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
    public class Player
        : StateBase, IPlayer
    {
        public Player(IGame game, string name, IPlayerDeck deck)
            : base(game)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (deck == null)
                throw new ArgumentNullException("deck");

            this.name = name;
            this.deck = deck;
            this.hand = new Hand<IPlayerCard>(game);
            this.currentThreat = deck.Threat;
        }

        private readonly string name;
        private readonly IPlayerDeck deck;
        private readonly IHand<IPlayerCard> hand;
        private readonly IDictionary<Guid, IAttachableInPlay> deckAttachments = new Dictionary<Guid, IAttachableInPlay>();
        private readonly IDictionary<Guid, ICardInPlay> cardsInPlay = new Dictionary<Guid, ICardInPlay>();
        private readonly IDictionary<Guid, IEnemyInPlay> engagedEnemies = new Dictionary<Guid, IEnemyInPlay>();

        private byte currentThreat;
        private bool isFirstPlayer;

        public string Name
        {
            get { return name; }
        }

        public IPlayerDeck Deck
        {
            get { return deck; }
        }

        public IHand<IPlayerCard> Hand
        {
            get { return hand; }
        }

        public IEnumerable<IAttachableInPlay> DeckAttachments
        {
            get { return deckAttachments.Values; }
        }

        public IEnumerable<ICardInPlay> CardsInPlay
        {
            get { return cardsInPlay.Values; }
        }

        public IEnumerable<IEnemyInPlay> EngagedEnemies
        {
            get { return engagedEnemies.Values; }
        }

        public byte CurrentThreat
        {
            get { return currentThreat; }
        }

        public bool IsFirstPlayer
        {
            get { return isFirstPlayer; }
            set
            {
                if (isFirstPlayer != value)
                {
                    isFirstPlayer = value;
                    OnPropertyChanged("IsFirstPlayer");
                }
            }
        }

        public void IncreaseThreat(byte value)
        {
            if (value == 0)
                return;

            currentThreat += value;
            OnPropertyChanged("CurrentThreat");
        }

        public void DecreaseThreat(byte value)
        {
            if (value == 0)
                return;

            currentThreat -= value;
            OnPropertyChanged("CurrentThreat");
        }

        public void DiscardFromHand(IEnumerable<IPlayerCard> cards)
        {
            Hand.RemoveCards(cards);
            Deck.Discard(cards);
        }

        public void AddDeckAttachment(IAttachableInPlay attachment)
        {
        }

        public void RemoveDeckAttachment(IAttachableInPlay attachment)
        {
        }

        public void AddCardInPlay(ICardInPlay card)
        {
        }

        public void RemoveCardInPlay(ICardInPlay card)
        {
        }

        public void AddEngagedEnemy(IEnemyInPlay enemy)
        {
        }

        public void RemoveEngagedEnemy(IEnemyInPlay enemy)
        {
        }

        public bool IsTheControllerOf(ICardInPlay card)
        {
            return false;
        }
    }
}
