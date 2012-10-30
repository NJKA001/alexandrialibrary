using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Locations;
using LotR.Cards.Quests;
using LotR.States.Phases.Any;

namespace LotR.States.Areas
{
    public interface IQuestArea
        : IArea
    {
        IDeck<IQuestCard> QuestDeck { get; }
        IQuestInPlay ActiveQuest { get; }

        IEnumerable<IDeck<IEncounterCard>> EncounterDecks { get; }
        IDeck<IEncounterCard> ActiveEncounterDeck { get; }

        ILocationInPlay ActiveLocation { get; }
        byte ActiveQuestProgress { get; }
        byte ActiveLocationProgress { get; }

        void SetActiveQuest(IQuestCard card);
        void SetActiveEncounterDeck(IDeck<IEncounterCard> deck);
        void SetActiveLocation(ILocationInPlay location);
        void RemoveActiveLocation();
        
        void AddProgress(byte value);
        void AddProgressBypassingActiveLocation(byte value);
        void RemoveProgressFromQuest(byte value);
        void RemoveProgressFromActiveLocation(byte value);

        void SetQuestCards();
        void SetupScenario();

        IQuestStage GetCurrentQuestStage();
    }
}
