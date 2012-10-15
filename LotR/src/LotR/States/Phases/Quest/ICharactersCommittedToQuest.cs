using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Phases.Quest
{
    public interface ICharactersCommittedToQuest
        : IState
    {
        bool IsCommittedToQuest(Guid cardId);
        IEnumerable<IWillpowerfulInPlay> GetAllCharactersCommittedToQuest();
        IEnumerable<IWillpowerfulInPlay> GetCharactersCommittedToQuest(Guid playerId);

        void CommitCharacterToQuest(IWillpowerfulInPlay character);
        void RemoveCharacterFromQuest(IWillpowerfulInPlay character);
    }
}
