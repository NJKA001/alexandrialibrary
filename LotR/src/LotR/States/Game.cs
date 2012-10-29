using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States.Areas;
using LotR.States.Phases;
using LotR.States.Phases.Resource;

namespace LotR.States
{
    public class Game
        : IGame
    {
        public Game()
        {
        }

        private readonly IList<IPlayer> players = new List<IPlayer>();
        private readonly IList<IEffect> currentEffects = new List<IEffect>();
        private readonly IList<Action<IEffect>> effectAddedCallbacks = new List<Action<IEffect>>();
        private readonly IList<Action<IEffect>> effectResolvedCallbacks = new List<Action<IEffect>>();
        private readonly IList<Action<IEffect, IPayment, IChoice>> paymentRejectedCallbacks = new List<Action<IEffect, IPayment, IChoice>>();

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnEffectAdded(IEffect effect)
        {
            foreach (var callback in effectAddedCallbacks)
                callback(effect);
        }

        private void OnEffectResolved(IEffect effect)
        {
            foreach (var callback in effectResolvedCallbacks)
                callback(effect);
        }

        private void OnPaymentRejected(IEffect effect, IPayment payment, IChoice choice)
        {
            foreach (var callback in paymentRejectedCallbacks)
                callback(effect, payment, choice);
        }

        public IPhase CurrentPhase
        {
            get;
            private set;
        }

        public IQuestArea QuestArea
        {
            get;
            private set;
        }

        public IStagingArea StagingArea
        {
            get;
            private set;
        }

        public IVictoryDisplay VictoryDisplay
        {
            get;
            private set;
        }

        public IEnumerable<IPlayer> Players
        {
            get { return players; }
        }

        public IPlayer FirstPlayer
        {
            get { return players.Single(x => x.IsFirstPlayer); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddEffect(IEffect effect)
        {
            if (effect == null)
                throw new ArgumentNullException("effect");

            currentEffects.Add(effect);

            OnEffectAdded(effect);
        }

        public void ResolveEffect(IEffect effect, IPayment payment, IChoice choice)
        {
            if (!effect.PaymentAccepted(this, payment, choice))
            {
                OnPaymentRejected(effect, payment, choice);
                return;
            }

            effect.Resolve(this, payment, choice);

            OnEffectResolved(effect);
        }

        public void Setup(IQuestArea questArea, IEnumerable<IPlayer> players)
        {
            if (questArea == null)
                throw new ArgumentNullException("questArea");
            if (players == null)
                throw new ArgumentNullException("players");
            if (players.Count() == 0)
                throw new ArgumentException("list of players cannot be empty");
            if (players.Any(x => x == null))
                throw new ArgumentException("list of players cannot contain nulls");

            this.QuestArea = questArea;
            this.StagingArea = new StagingArea(this, questArea.ActiveEncounterDeck);
            this.VictoryDisplay = new VictoryDisplay(this);

            foreach (var player in players)
            {
                this.players.Add(player);

                foreach (var card in player.Deck.Cards)
                {
                    card.Owner = player;
                }

                foreach (var hero in player.Deck.Heroes)
                {
                    player.AddCardInPlay(new HeroInPlay(this, hero));
                }

                player.Deck.Shuffle();
                player.DrawCards(6);
            }

            this.QuestArea.Setup();

            foreach (var setupEffect in currentEffects.OfType<ISetupEffect>().ToList())
            {
                setupEffect.Setup(this);
                var choice = setupEffect.GetChoice(this);
                var cost = setupEffect.GetCost(this);
                if (choice == null && cost == null)
                {
                    ResolveEffect(setupEffect, null, choice);
                    currentEffects.Remove(setupEffect);
                }
            }

            players.First().IsFirstPlayer = true;

            CurrentPhase = new ResourcePhase(this);
        }

        public void RegisterChoiceCallback(Action<IEffect, IChoice> callback)
        {
        }

        public void RegisterPaymentCallback(Action<IEffect, IPayment> callback)
        {
        }

        public void RegisterEffectAddedCallback(Action<IEffect> callback)
        {
            if (callback == null)
                throw new ArgumentException("callback");

            effectAddedCallbacks.Add(callback);
        }

        public void RegisterEffectResolvedCallback(Action<IEffect> callback)
        {
            if (callback == null)
                throw new ArgumentException("callback");

            effectResolvedCallbacks.Add(callback);
        }

        public void RegisterPaymentRejectedCallback(Action<IEffect, IPayment, IChoice> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            paymentRejectedCallbacks.Add(callback);
        }

        public T GetCardInPlay<T>(Guid cardId)
            where T : class, ICardInPlay
        {
            T card = null;

            if (QuestArea.ActiveLocation.Card.Id == cardId)
                return QuestArea.ActiveLocation as T;

            if (QuestArea.ActiveQuest.Card.Id == cardId)
                return QuestArea.ActiveQuest as T;

            card = StagingArea.CardsInStagingArea.OfType<T>().Where(x => x.StateId == cardId).FirstOrDefault();
            if (card != null)
                return card;

            foreach (var player in players)
            {
                card = player.CardsInPlay.OfType<T>().Where(x => x.StateId == cardId).FirstOrDefault();
                if (card != null)
                    return card;
            }

            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("\r\nCurrent Phase: {0}\r\n", CurrentPhase.Code);
            sb.AppendFormat("Current Step: {0}\r\n", CurrentPhase.Step);
            sb.AppendFormat("First Player: {0}\r\n", FirstPlayer.Name);

            if (currentEffects.Count > 0)
            {
                sb.AppendFormat("Current Effects:\r\n", currentEffects.Count);

                var seq = 0;
                foreach (var effect in currentEffects)
                {
                    seq++;
                    sb.AppendFormat("{0,00}  {1}", seq, effect);
                }
            }
            else
            {
                sb.AppendLine("\r\nNo Current Effects");
            }

            return sb.ToString();
        }
    }
}
