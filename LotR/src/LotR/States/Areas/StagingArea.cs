using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Enemies;
using LotR.Cards.Encounter.Locations;
using LotR.Cards.Encounter.Objectives;
using LotR.Cards.Encounter.Treacheries;
using LotR.Effects;
using LotR.Effects.Phases.Any;

namespace LotR.States.Areas
{
    public class StagingArea
        : AreaBase, IStagingArea
    {
        public StagingArea(IGame game)
            : base(game)
        {
        }

        private readonly IList<Action<IEncounterInPlay>> cardAddedToStagingAreaCallbacks = new List<Action<IEncounterInPlay>>();
        private readonly IList<Action<IEncounterInPlay>> cardRemovedFromStagingAreaCallbacks = new List<Action<IEncounterInPlay>>();
        private readonly ObservableCollection<IEncounterInPlay> cardsInStagingArea = new ObservableCollection<IEncounterInPlay>();

        private IDeck<IEncounterCard> encounterDeck;
        private IEncounterInPlay revealedEncounterCard;
        private ICancelEffect cancelWhenRevealedEffect;

        public IDeck<IEncounterCard> EncounterDeck
        {
            get { return encounterDeck; }
            private set
            {
                encounterDeck = value;
                OnPropertyChanged("EncounterDeck");
            }
        }

        public IEncounterInPlay RevealedEncounterCard
        {
            get { return revealedEncounterCard; }
            private set
            {
                revealedEncounterCard = value;
                OnPropertyChanged("RevealedEncounterCard");
            }
        }

        public IEnumerable<IEncounterInPlay> CardsInStagingArea
        {
            get { return cardsInStagingArea; }
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

        private void CheckForResponsesToRevealedCard()
        {
            if (!RevealedEncounterCard.HasEffect<IWhenRevealedEffect>())
                return;

            var duringRevealCards = new List<ICard>();

            duringRevealCards.AddRange(Game.GetCardsInPlayWithEffect<ICardInPlay, IDuringEncounterCardRevealed>().Select(x => x.BaseCard));

            foreach (var player in Game.Players)
            {
                duringRevealCards.AddRange(player.Hand.Cards.Where(x => x.HasEffect<IDuringEncounterCardRevealed>()));
            }

            foreach (var card in duringRevealCards)
            {

            }
        }

        private IEncounterInPlay GetRevealedEncounterCard(IEncounterCard card)
        {
            if (card is IEnemyCard)
            {
                return new EnemyInPlay(Game, card as IEnemyCard);
            }
            else if (card is ILocationCard)
            {
                return new LocationInPlay(Game, card as ILocationCard);
            }
            else if (card is IObjectiveCard)
            {
                return new UnclaimedObjectiveInPlay(Game, card as IObjectiveCard);
            }
            else if (card is ITreacheryCard)
            {
                return new TreacheryInPlay(Game, card as ITreacheryCard);
            }
            else throw new ArgumentException("card is not a valid encounter card");
        }

        private void TriggerWhenRevealedEffects(IEncounterCard card)
        {
            foreach (var effect in card.Text.Effects.Where(x => x is IWhenRevealedEffect))
            {
                var handle = effect.GetHandle(Game);
                Game.TriggerEffect(handle);
            }
        }

        private void TriggerOtherEffects(IEncounterCard card)
        {
            foreach (var effect in card.Text.Effects.Where(x => x is IRevealedEffect  && !(x is IWhenRevealedEffect)))
            {
                var handle = effect.GetHandle(Game);
                Game.TriggerEffect(handle);
            }
        }

        public void RevealEncounterCard()
        {
            if (EncounterDeck.Cards.Count() == 0)
            {
                if (Game.CurrentPhase.Code == PhaseCode.Quest)
                {
                    EncounterDeck.ShuffleDiscardPileIntoDeck();
                }
                else
                {
                    return;
                }
            }

            cancelWhenRevealedEffect = null;

            var card = EncounterDeck.GetFromTop(1).First();
            EncounterDeck.RemoveFromDeck(card);

            RevealedEncounterCard = GetRevealedEncounterCard(card);

            CheckForResponsesToRevealedCard();

            TriggerWhenRevealedEffects(card);
            
            TriggerOtherEffects(card);

            if (!(RevealedEncounterCard.Card is ITreacheryCard))
            {
                AddToStagingArea(RevealedEncounterCard);
            }

            RevealedEncounterCard = null;
        }

        public void CancelRevealedCard(ICancelEffect effect)
        {
            if (effect == null)
                throw new ArgumentNullException("effect");

            cancelWhenRevealedEffect = effect;
        }

        public void RemoveRevealedCard()
        {
            RevealedEncounterCard = null;
        }

        private void AddToStagingArea(IEncounterInPlay encounterInPlay)
        {
            cardsInStagingArea.Add(encounterInPlay);

            foreach (var callback in cardAddedToStagingAreaCallbacks)
            {
                callback(encounterInPlay);
            }
        }

        public void AddToStagingArea(IEncounterCard card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            IEncounterInPlay encounterInPlay = null;

            if (card is IEnemyCard)
            {
                encounterInPlay = new EnemyInPlay(Game, card as IEnemyCard);
            }
            else if (card is ILocationCard)
            {
                encounterInPlay = new LocationInPlay(Game, card as ILocationCard);
            }
            else if (card is IObjectiveCard)
            {
                encounterInPlay = new UnclaimedObjectiveInPlay(Game, card as IObjectiveCard);
            }

            AddToStagingArea(encounterInPlay);
        }

        public void RemoveFromStagingArea(IEncounterInPlay card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            if (!cardsInStagingArea.Contains(card))
                return;

            cardsInStagingArea.Remove(card);

            foreach (var callback in cardRemovedFromStagingAreaCallbacks)
            {
                callback(card);
            }
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

        public void RegisterCardAddedToStagingAreaCallback(Action<IEncounterInPlay> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            cardAddedToStagingAreaCallbacks.Add(callback);
        }

        public void RegisterCardRemovedFromStagingAreaCallback(Action<IEncounterInPlay> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            cardRemovedFromStagingAreaCallbacks.Add(callback);
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
