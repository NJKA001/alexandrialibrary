﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Quests;
using LotR.Cards.Encounter;
using LotR.Effects;
using LotR.States.Phases.Any;

namespace LotR.States.Areas
{
    public class QuestArea
        : AreaBase, IQuestArea
    {
        public QuestArea(IGame game, IDeck<IQuestCard> questDeck, IEnumerable<IDeck<IEncounterCard>> encounterDecks)
            : base(game)
        {
            if (questDeck == null)
                throw new ArgumentNullException("questDeck");
            if (questDeck.Cards.Count() == 0)
                throw new ArgumentException("questDeck does not contain any cards");
            if (encounterDecks == null)
                throw new ArgumentNullException("encounterDecks");
            if (encounterDecks.Count() == 0)
                throw new ArgumentException("encounterDecks is an empty list");

            this.QuestDeck = questDeck;
            this.EncounterDecks = encounterDecks;
        }

        private byte activeLocationProgress;
        private byte activeQuestProgress;
        private IQuestInPlay activeQuest;
        private ILocationInPlay activeLocation;

        public IDeck<IQuestCard> QuestDeck
        {
            get;
            private set;
        }

        public IQuestInPlay ActiveQuest
        {
            get { return activeQuest; }
            private set
            {
                activeQuest = value;
                OnPropertyChanged("ActiveQuest");
            }
        }

        public IEnumerable<IDeck<IEncounterCard>> EncounterDecks
        {
            get;
            private set;
        }

        public IDeck<IEncounterCard> ActiveEncounterDeck
        {
            get;
            private set;
        }

        public ILocationInPlay ActiveLocation
        {
            get { return activeLocation; }
            private set
            {
                activeLocation = value;
                OnPropertyChanged("ActiveLocation");
            }
        }

        public byte ActiveQuestProgress
        {
            get { return activeQuestProgress; }
            private set
            {
                if (value < 0)
                    activeQuestProgress = 0;
                else if (value > 255)
                    activeQuestProgress = 255;
                else 
                    activeQuestProgress = value;

                OnPropertyChanged("ActiveQuestProgress");
            }
        }

        public byte ActiveLocationProgress
        {
            get { return activeLocationProgress; }
            private set
            {
                if (value < 0)
                    activeLocationProgress = 0;
                if (value > 255)
                    activeLocationProgress = 255;
                else
                    activeLocationProgress = value;
            }   
        }

        public void SetActiveQuest(IQuestCard card)
        {
            if (!QuestDeck.Cards.Contains(card))
                return;

            ActiveQuest = new QuestInPlay(Game, card);
        }

        public void SetActiveEncounterDeck(IDeck<IEncounterCard> deck)
        {
            if (!EncounterDecks.Contains(deck))
                return;

            ActiveEncounterDeck = deck;
        }

        public void SetActiveLocation(ILocationInPlay location)
        {
            ActiveLocation = location;
        }

        public void RemoveActiveLocation()
        {
            ActiveLocation = null;
        }

        public void AddProgress(byte value)
        {
            if (ActiveLocation != null)
            {
                ActiveLocationProgress += value;
            }
            else
            {
                ActiveQuestProgress += value;
            }
        }

        public void AddProgressBypassingActiveLocation(byte value)
        {
            ActiveQuestProgress += value;
        }

        public void RemoveProgressFromQuest(byte value)
        {
            ActiveQuestProgress -= value;
        }

        public void RemoveProgressFromActiveLocation(byte value)
        {
            ActiveLocationProgress -= value;
        }

        public void SetQuestCards()
        {
            QuestDeck.Order(x => x.Sequence);
            SetActiveQuest(QuestDeck.Cards.First());
            SetActiveEncounterDeck(EncounterDecks.First());
        }

        public void SetupScenario()
        {
            if (ActiveQuest.HasEffect<ISetupEffect>())
            {
                foreach (var setupEffect in ActiveQuest.Card.Text.Effects.OfType<ISetupEffect>())
                {
                    Game.AddEffect(setupEffect);
                }
            }
        }

        public IQuestStatus GetCurrentQuestStage()
        {
            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (ActiveQuest != null)
            {
                sb.AppendFormat("\r\nActive Quest: {0}\r\n", ActiveQuest.Title);
                sb.AppendFormat("Progress: {0} of {1}\r\n", ActiveQuest.Progress, ActiveQuest.Card.QuestPoints);
                sb.AppendLine("Text:\r\n");
                foreach (var effect in ActiveQuest.Card.Text.Effects)
                {
                    sb.AppendFormat("{0}\r\n", effect);
                }
            }
            else
            {
                sb.AppendLine("No Active Quest");
            }

            if (ActiveLocation != null)
            {
                sb.AppendFormat("\r\nActive Location: {0} ({1} threat)\r\n", ActiveLocation.Title, ((IThreateningInPlay)ActiveLocation).Threat);
                sb.AppendFormat("Progress: {0} of {1}\r\n", ActiveLocation.Progress, ActiveLocation.Card.QuestPoints);
            }
            else
            {
                sb.AppendLine("No Active Location");
            }

            return sb.ToString();
        }
    }
}
