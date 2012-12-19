using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Quest
{
    public interface IQuestOutcome
        : IState
    {
        IEnumerable<Guid> Players { get; }
        IEnumerable<IEncounterInPlay> RevealedEncounterCards { get; }

        uint TotalWillpower { get; }
        uint TotalThreat { get; }
        
        bool IsQuestSuccessful { get; }
        bool IsQuestFailed { get; }
        bool IsQuestIndeterminate { get; }

        byte GetThreatIncrease(Guid playerId);
        void SetThreatIncrease(Guid playerId, byte value);

        void Resolve(uint totalWillpower, uint totalThreat);
        void EncounterCardRevealed(IEncounterInPlay encounterCardInPlay);
    }
}
