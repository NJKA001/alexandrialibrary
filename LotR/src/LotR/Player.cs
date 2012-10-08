using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR
{
    public class Player
        : IPlayer
    {
        public Player(string name, IEnumerable<IHeroCard> startingHeroes, IEnumerable<IPlayerCard> cards)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (startingHeroes == null)
                throw new ArgumentNullException("startingHeroes");
            if (cards == null)
                throw new ArgumentNullException("cards");

            this.name = name;
            this.startingHeroes = startingHeroes;
            this.deck = new Deck<IPlayerCard>(cards);
            this.startingThreat = (byte)startingHeroes.Sum(x => x.ThreatCost);
            this.CurrentThreat = startingThreat;
            
            
            //this.heroesInPlay.Add(
        }

        private readonly string name;
        private readonly IEnumerable<IHeroCard> startingHeroes;
        //private readonly IEnumerable<IPlayerCard> cards;
        private readonly IDeck<IPlayerCard> deck;
        private readonly byte startingThreat;
        
        private readonly IList<IHeroInPlay> heroesInPlay = new List<IHeroInPlay>();
        private readonly IList<ICardInPlay> cardsInPlay = new List<ICardInPlay>();
        private readonly IList<IPlayerCard> cardsInHand = new List<IPlayerCard>();
        private readonly IDeck<IPlayerCard> discardPile = new Deck<IPlayerCard>();
        private readonly IList<IEnemyInPlay> engagedEnemies = new List<IEnemyInPlay>();

        public string Name
        {
            get { return name; }
        }

        public byte StartingThreat
        {
            get { return startingThreat; }
        }

        public IEnumerable<IHeroCard> StartingHeroes
        {
            get { return startingHeroes; }
        }

        public byte CurrentThreat
        {
            get;
            private set;
        }

        public IEnumerable<IHeroInPlay> HeroesInPlay
        {
            get { return heroesInPlay; }
        }

        public IEnumerable<ICardInPlay> CardsInPlay
        {
            get { return cardsInPlay; }
        }

        public IEnumerable<IPlayerCard> CardsInHand
        {
            get { return cardsInHand; }
        }

        public IEnumerable<IEnemyInPlay> EngagedEnemies
        {
            get { return engagedEnemies; }
        }

        public IDeck<IPlayerCard> Deck
        {
            get { return deck; }
        }

        public IDeck<IPlayerCard> DiscardPile
        {
            get { return discardPile; }
        }

        public bool IsFirstPlayer
        {
            get { throw new NotImplementedException(); }
        }

        public void IncreaseThreat(byte value)
        {
            throw new NotImplementedException();
        }

        public void DecreaseThreat(byte value)
        {
            throw new NotImplementedException();
        }

        public void AddCardsToHand(IEnumerable<IPlayerCard> cards)
        {
            throw new NotImplementedException();
        }

        public void RemoveCardsFromHand(IEnumerable<IPlayerCard> cards)
        {
            throw new NotImplementedException();
        }

        public void DrawCards(byte value)
        {
            throw new NotImplementedException();
        }

        #region INotifyPropertyChanged Members

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
