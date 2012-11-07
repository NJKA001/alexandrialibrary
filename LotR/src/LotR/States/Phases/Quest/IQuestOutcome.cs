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
        
        bool IsQuestSuccessful { get; set; }
        bool IsQuestUnsuccessful { get; set; }

        byte GetThreatIncrease(Guid playerId);
        void SetThreatIncrease(Guid playerId);
    }
}
