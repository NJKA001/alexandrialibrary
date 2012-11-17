using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;
using LotR.Effects;
using LotR.Effects.Phases.Any;

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
        }

        private readonly string name;
        private readonly IPlayerDeck deck;
        private readonly IHand<IPlayerCard> hand;
        private readonly ObservableCollection<IAttachableInPlay> deckAttachments = new ObservableCollection<IAttachableInPlay>();
        private readonly ObservableCollection<ICardInPlay> cardsInPlay = new ObservableCollection<ICardInPlay>();
        private readonly ObservableCollection<IEnemyInPlay> engagedEnemies = new ObservableCollection<IEnemyInPlay>();

        private byte currentThreat;
        private bool isFirstPlayer;
        private bool isActivePlayer;

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
            get { return deckAttachments; }
        }

        public IEnumerable<ICardInPlay> CardsInPlay
        {
            get { return cardsInPlay; }
        }

        public IEnumerable<IEnemyInPlay> EngagedEnemies
        {
            get { return engagedEnemies; }
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
                if (isFirstPlayer == value)
                    return;
                
                isFirstPlayer = value;
                OnPropertyChanged("IsFirstPlayer");
            }
        }

        public bool IsActivePlayer
        {
            get { return isActivePlayer; }
            set
            {
                if (isActivePlayer == value)
                    return;

                isActivePlayer = value;
                OnPropertyChanged("IsActivePlayer");
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
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (deckAttachments.Contains(attachment))
                return;

            deckAttachments.Add(attachment);
        }

        public void RemoveDeckAttachment(IAttachableInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (!deckAttachments.Contains(attachment))
                return;

            deckAttachments.Remove(attachment);
        }

        public void AddCardInPlay(ICardInPlay card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            if (cardsInPlay.Contains(card))
                return;

            cardsInPlay.Add(card);

            var entersPlayEffect = new CardEntersPlayEffect(Game, card);
            var entersPlayHandle = entersPlayEffect.GetHandle(Game);
            Game.AddEffect(entersPlayEffect);
            Game.TriggerEffect(entersPlayHandle);
        }

        public void RemoveCardInPlay(ICardInPlay card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            if (!cardsInPlay.Contains(card))
                return;

            cardsInPlay.Remove(card);

            var leavesPlayEffect = new CardLeavesPlayEffect(Game, card);
            var leavesPlayHandle = leavesPlayEffect.GetHandle(Game);
            Game.AddEffect(leavesPlayEffect);
            Game.TriggerEffect(leavesPlayHandle);
        }

        public void AddEngagedEnemy(IEnemyInPlay enemy)
        {
            if (enemy == null)
                throw new ArgumentNullException("enemy");

            if (engagedEnemies.Contains(enemy))
                return;

            engagedEnemies.Add(enemy);
        }

        public void RemoveEngagedEnemy(IEnemyInPlay enemy)
        {
            if (enemy == null)
                throw new ArgumentNullException("enemy");

            if (!engagedEnemies.Contains(enemy))
                return;

            engagedEnemies.Remove(enemy);
        }

        public void DrawCards(uint numberOfCards)
        {
            Deck.Draw(numberOfCards, (cardsToDraw) => Hand.AddCards(cardsToDraw));
        }

        public bool IsTheControllerOf(ICardInPlay card)
        {
            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (IsFirstPlayer)
                sb.AppendFormat("\r\n{0} ({1} threat)\r\n*** First Player ***\r\n", Name, CurrentThreat);
            else
                sb.AppendFormat("\r\n{0} ({1} threat)\r\n", Name, CurrentThreat);

            var handCount = Hand.Cards.Count();
            var deckCount = Deck.Cards.Count();
            var discardCount = Deck.DiscardPile.Count();

            var heroCount = cardsInPlay.OfType<IHeroInPlay>().Count();
            var allyCount = cardsInPlay.OfType<IAllyInPlay>().Count();

            sb.AppendFormat("Cards in Hand: {0}\r\n", handCount);
            sb.AppendFormat("Cards in Deck: {0}\r\n", deckCount);
            sb.AppendFormat("Cards in Discard Pile: {0}\r\n", discardCount);
            sb.AppendFormat("Heroes in Play: {0}\r\n", heroCount);

            var number = 0;
            foreach (var hero in cardsInPlay.OfType<IHeroInPlay>())
            {
                number++;
                var currentHitPoints = hero.Card.PrintedHitPoints - hero.Damage;
                sb.AppendFormat("{0,00}  {1} ({2} resources, {3} of {4} hit points)\r\n", number, hero.Title, hero.Resources, currentHitPoints, hero.Card.PrintedHitPoints);
            }

            if (allyCount > 0)
            {
                sb.AppendFormat("Allies in Play: {0}\r\n", allyCount);
                foreach (var ally in cardsInPlay.OfType<IAllyInPlay>())
                {
                    number++;
                    var currentHitPoints = ally.Card.PrintedHitPoints - ally.Damage;
                    sb.AppendFormat("{0,00}  {1} ({2} of {3} hit points)\r\n", number, ally.Title, currentHitPoints, ally.Card.PrintedHitPoints);
                }
            }

            return sb.ToString();
        }
    }
}
