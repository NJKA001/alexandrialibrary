using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Quests;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Games
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
        IVictoryDisplayArea VictoryDisplay { get; }
        IOutOfPlayArea OutOfPlay { get; }

        void ChangeActiveLocation(ILocationInPlay location);
        void ClearActiveLocation();
        void ChangeActiveQuest(IQuestCardInPlay quest);

        ICard GetCard(Guid id);
        ICardInPlay GetCardInPlay(Guid id);
        IPlayer GetController(Guid id);
        IPlayer GetOwner(Guid id);
        int GetScore();

        bool IsComplete { get; }
        void Start();
        void Complete();
        void RevealEncounterCards(byte numberOfCards);
    }
}
