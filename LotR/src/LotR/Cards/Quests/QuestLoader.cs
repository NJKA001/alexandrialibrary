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
            InitializeCards();
        }

        private readonly IList<IEncounterCard> encounterCards = new List<IEncounterCard>();
        private readonly IList<IQuestCard> questCards = new List<IQuestCard>();
        private readonly IList<IScenario> scenarios = new List<IScenario>();

        private void InitializeCards()
        {
            InitializeQuestCards();
            InitializeEncounterCards();
        }

        private void InitializeQuestCards()
        {
            foreach (var type in GetQuestCards())
            {
                var ctor = type.GetConstructor(new Type[0]);
                if (ctor == null)
                    continue;

                var questCard = ctor.Invoke(null) as IQuestCard;
                if (questCard == null)
                    continue;

                questCards.Add(questCard);
            }
        }

        private void InitializeEncounterCards()
        {
            foreach (var type in GetEncounterCards())
            {
                var ctor = type.GetConstructor(new Type[0]);
                if (ctor == null)
                    continue;

                var encounterCard = ctor.Invoke(null) as IEncounterCard;
                if (encounterCard == null)
                    continue;

                encounterCards.Add(encounterCard);
            }
        }

        private IEnumerable<Type> GetQuestCards()
        {
            return GetTypesImplementingInterface(typeof(IQuestCard)).Where(x => x != null && IsRealClass(x));
        }

        private IEnumerable<Type> GetEncounterCards()
        {
            return GetTypesImplementingInterface(typeof(IEncounterCard)).Where(x => x != null && IsRealClass(x));
        }

        public IEnumerable<IEncounterCard> EncounterCards
        {
            get { return encounterCards; }
        }

        public IEnumerable<IQuestCard> QuestCards
        {
            get { return questCards; }
        }

        public IQuestArea Load(IGame game, ScenarioCode scenario)
        {
            var quests = GetCards<IQuestCard>(questCards);

            var questDeck = new Deck<IQuestCard>(quests);
            var encounterDecks = new List<IDeck<IEncounterCard>>();

            IEnumerable<EncounterSet> encounterSets = Enumerable.Empty<EncounterSet>();

            foreach (var quest in quests)
            {
                if (!quest.EncounterSets.SequenceEqual(encounterSets))
                {
                    var encounters = GetCards(encounterCards.Where(x => quest.EncounterSets.Contains(x.EncounterSet)));
                    var encounterDeck = new Deck<IEncounterCard>(encounters);
                    encounterDeck.Shuffle();

                    encounterDecks.Add(encounterDeck);
                }

                encounterSets = quest.EncounterSets;
            }

            return new QuestArea(game, questDeck, encounterDecks);
        }
    }
}
