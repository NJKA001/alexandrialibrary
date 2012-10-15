using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Quests
{
    public abstract class QuestCardBase
        : CardBase, IQuestCard
    {
        protected QuestCardBase(string title, CardSet cardSet, uint cardNumber, Scenario scenario, IEnumerable<EncounterSet> encounterSets, byte sequence, byte questPoints, byte victoryPoints)
            : base(title, cardSet, cardNumber)
        {
            if (encounterSets == null)
                throw new ArgumentNullException("encounterSets");

            this.Scenario = scenario;
            this.EncounterSets = encounterSets;
            this.Sequence = sequence;
            this.QuestPoints = questPoints;
            this.VictoryPoints = victoryPoints;
        }

        public Scenario Scenario
        {
            get;
            private set;
        }

        public IEnumerable<EncounterSet> EncounterSets
        {
            get;
            private set;
        }

        public byte Sequence
        {
            get;
            private set;
        }

        public byte QuestPoints
        {
            get;
            private set;
        }

        public byte VictoryPoints
        {
            get;
            private set;
        }
    }
}
