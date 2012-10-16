using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Cards.Quests
{
    public class QuestLoader
        : LoaderBase, IQuestLoader
    {
        public QuestLoader()
        {
        }

        private readonly IList<IEncounterCard> encounterCards = new List<IEncounterCard>();
        private readonly IList<IQuestCard> questCards = new List<IQuestCard>();

        public IEnumerable<IEncounterCard> EncounterCards
        {
            get { return encounterCards; }
        }

        public IEnumerable<IQuestCard> QuestCards
        {
            get { return questCards; }
        }

        public IQuestArea Load(Scenario scenario)
        {
            var questDeck = new Deck<IQuestCard>();
            var encounterDecks = new List<IDeck<IEncounterCard>>();

            return new QuestArea(questDeck, encounterDecks);
        }
    }
}
