using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Quest
{
    public interface IQuestPhase
        : IPhase
    {
        byte NumberOfEncounterCardsToReveal { get; set; }

        bool IsCommittedToQuest(Guid cardId);
        bool IsExhaustedToQuest(Guid cardId);
        IEnumerable<IWillpowerfulInPlay> GetAllCharactersCommittedToQuest();
        IEnumerable<IWillpowerfulInPlay> GetCharactersCommitedToTheQuest(Guid playerId);

        void CommitCharacterToQuest(IWillpowerfulInPlay character);
        void RemoveCharacterFromQuest(IWillpowerfulInPlay character);
        void CharacterExhaustsToQuest(Guid cardId);
        void CharacterDoesNotExhaustToQuest(Guid cardId);
    }
}
