using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Quests;
using LotR.Cards.Encounter;
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
            if (encounterDecks == null)
                throw new ArgumentNullException("encounterDecks");

            this.QuestDeck = questDeck;
            this.EncounterDecks = encounterDecks;
        }

        private byte activeLocationProgress;
        private byte activeQuestProgress;

        public IDeck<IQuestCard> QuestDeck
        {
            get;
            private set;
        }

        public IQuestInPlay ActiveQuest
        {
            get;
            private set;
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
            get;
            private set;
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

            ActiveQuest = new QuestInPlay(game, card);
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

        public void Setup()
        {
        }

        public IQuestStage GetCurrentQuestStage()
        {
            return null;
        }
    }
}
