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
        public Player(string name, IPlayerDeck deck)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (deck == null)
                throw new ArgumentNullException("deck");

            this.name = name;
            this.deck = deck;
            this.currentThreat = deck.Threat;
        }

        private readonly string name;
        private readonly IPlayerDeck deck;
        private readonly IHand<IPlayerCard> hand = new Hand<IPlayerCard>();

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
    }
}
