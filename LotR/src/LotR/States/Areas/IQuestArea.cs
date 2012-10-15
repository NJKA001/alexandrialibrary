using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Locations;
using LotR.Cards.Quests;

namespace LotR.States.Areas
{
    public interface IQuestArea
        : IArea
    {
        IDeck<IQuestCard> QuestDeck { get; }
        IQuestCard ActiveQuest { get; }
        ILocationInPlay ActiveLocation { get; }
        byte ActiveQuestProgress { get; }
        byte ActiveLocationProgress { get; }

        void SetActiveQuest(IQuestCard card);
        void AddActiveQuestProgress(byte value);
        void RemoveActiveQuestProgress(byte value);

        void SetActiveLocation(ILocationInPlay location);
        void RemoveActiveLocation();
        void AddActiveLocationProgress(byte value);
        void RemoveActiveLocationProgress(byte value);

        void AddProgress(byte value);
        void RemoveProgress(byte value);
    }
}
