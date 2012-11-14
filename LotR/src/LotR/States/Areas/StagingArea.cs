using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Enemies;
using LotR.Cards.Encounter.Locations;
using LotR.Cards.Encounter.Objectives;
using LotR.Effects;

namespace LotR.States.Areas
{
    public class StagingArea
        : AreaBase, IStagingArea
    {
        public StagingArea(IGame game)
            : base(game)
        {
        }

        private readonly ObservableCollection<IEncounterInPlay> cardsInStagingArea = new ObservableCollection<IEncounterInPlay>();
        private readonly ObservableCollection<IEncounterCard> examinedEncounterCards = new ObservableCollection<IEncounterCard>();

        public IDeck<IEncounterCard> EncounterDeck
        {
            get;
            private set;
        }

        public IEncounterCard RevealedEncounterCard
        {
            get;
            private set;
        }

        public IEnumerable<IEncounterInPlay> CardsInStagingArea
        {
            get { return cardsInStagingArea; }
        }

        public IEnumerable<IEncounterCard> ExaminedEncounterCards
        {
            get { return examinedEncounterCards; }
        }

        public void AddExaminedEncounterCards(IEnumerable<IEncounterCard> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");

            foreach (var card in cards)
            {
                if (examinedEncounterCards.Contains(card))
                    continue;

                examinedEncounterCards.Add(card);
            }
        }

        public void RemoveExaminedEncounterCards(IEnumerable<IEncounterCard> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");

            foreach (var card in cards)
            {
                if (!examinedEncounterCards.Contains(card))
                    continue;

                examinedEncounterCards.Remove(card);
            }
        }

        public void ChangeEncounterDeck(IDeck<IEncounterCard> encounterDeck)
        {
            if (encounterDeck == null)
                throw new ArgumentNullException("encounterDeck");

            if (this.EncounterDeck != null)
            {
                EncounterDeck.ShuffleIn(cardsInStagingArea.Select(x => x.Card));
                cardsInStagingArea.Clear();
                this.EncounterDeck.ShuffleDiscardPileIntoDeck();
            }

            this.EncounterDeck = encounterDeck;
        }

        public void RevealEncounterCards(byte numberOfCards)
        {
            if (numberOfCards == 0)
                throw new ArgumentException("numberOfCards must be greater than zero");

        }

        public void CancelRevealedCard(ICancelEffect effect)
        {
            if (effect == null)
                throw new ArgumentNullException("effect");

        }

        public void AddToStagingArea(IEncounterCard card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            if (card is IEnemyCard)
            {
                cardsInStagingArea.Add(new EnemyInPlay(Game, card as IEnemyCard));
            }
            else if (card is ILocationCard)
            {
                cardsInStagingArea.Add(new LocationInPlay(Game, card as ILocationCard));
            }
            else if (card is IObjectiveCard)
            {
                cardsInStagingArea.Add(new UnclaimedObjectiveInPlay(Game, card as IObjectiveCard));
            }
        }

        public void RemoveFromStagingArea(IEncounterInPlay card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            if (!cardsInStagingArea.Contains(card))
                return;

            cardsInStagingArea.Remove(card);
        }

        public void AddToEncounterDiscardPile(IEnumerable<IEncounterCard> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");
        }

        public void AddToTopOfEncounterDeck(IEnumerable<IEncounterCard> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");
        }

        public void AddToBottomOfEncounterDeck(IEnumerable<IEncounterCard> cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");
        }

        public byte GetTotalThreat()
        {
            return (byte)cardsInStagingArea.OfType<IThreateningInPlay>().Sum(x => x.Threat);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Staging Area: {0} cards ({1} threat)\r\n", cardsInStagingArea.Count, GetTotalThreat());

            var number = 0;
            var locationCount = cardsInStagingArea.OfType<ILocationInPlay>().Count();
            var locationThreat = cardsInStagingArea.OfType<ILocationInPlay>().OfType<IThreateningInPlay>().Sum(x => x.Threat);
            var enemyCount = cardsInStagingArea.OfType<IEnemyInPlay>().Count();
            var enemyThreat = cardsInStagingArea.OfType<IEnemyInPlay>().OfType<IThreateningInPlay>().Sum(x => x.Threat);
            var objectiveCount = cardsInStagingArea.OfType<IObjectiveInPlay>().Count();

            if (locationCount > 0)
            {
                sb.AppendFormat("  Locations: {0} cards ({1} threat)\r\n", locationCount, locationThreat);
                foreach (var location in cardsInStagingArea.OfType<ILocationInPlay>().OfType<IThreateningInPlay>())
                {
                    number++;
                    sb.AppendFormat("{0,00}  {1} ({2} threat)\r\n", number, location.Card.Title, location.Threat);
                }
            }

            if (enemyCount > 0)
            {
                sb.AppendFormat("  Enemies: {0} ({1} threat)\r\n", enemyCount, enemyThreat);
                foreach (var enemy in cardsInStagingArea.OfType<IEnemyInPlay>())
                {
                    number++;
                    sb.AppendFormat("{0,00}  {1} ({2} threat, {3}/{4} hit points)\r\n", number, enemy.Card.Title, enemy.Card.PrintedThreat, enemy.Card.PrintedHitPoints - enemy.Damage, enemy.Card.PrintedHitPoints);
                }
            }

            if (objectiveCount > 0)
            {
                sb.AppendFormat("  Objectives: {0}", objectiveCount);
                foreach (var objective in cardsInStagingArea.OfType<IObjectiveInPlay>())
                {
                    sb.AppendFormat("{0,00}  {1}", number, objective.Card.Title);
                }
            }

            return sb.ToString();
        }
    }
}
