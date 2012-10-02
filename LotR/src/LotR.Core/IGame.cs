using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IGame
    {
        IScenario Scenario { get; }
        IEnumerable<IPlayer> Players { get; }
        
        IPlayer FirstPlayer { get; }
        IRound CurrentRound { get; }
        IEnumerable<IRound> PreviousRounds { get; }


        IDeck<IQuestCard> QuestDeck { get; }

        IStagingArea StagingArea { get; }
        ILocationInPlay ActiveLocation { get; }
        IQuestCardInPlay ActiveQuest { get; }
        IVictoryDisplay VictoryDisplay { get; }
        IOutOfPlayArea OutOfPlay { get; }

        void ChangeActiveLocation(ILocationInPlay location);
        void ClearActiveLocation();
        void ChangeActiveQuest(IQuestCardInPlay quest);

        ICard GetCard(Guid id);
        ICardInPlay GetCardInPlay(Guid id);
        int GetScore();

        bool IsComplete { get; }
        void Start();
        void Complete();
        void RevealEncounterCards(byte numberOfCards);
    }
}
