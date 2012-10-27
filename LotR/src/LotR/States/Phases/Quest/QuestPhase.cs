using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Quest
{
    public class QuestPhase
        : PhaseBase, IQuestPhase
    {
        public QuestPhase(IGame game)
            : base(game, PhaseCode.Quest, PhaseStep.Quest_Start)
        {
        }

        private readonly IDictionary<Guid, IList<IWillpowerfulInPlay>> committedToQuest = new Dictionary<Guid, IList<IWillpowerfulInPlay>>();

        public bool IsCommittedToQuest(Guid cardId)
        {
            return committedToQuest.Any(x => x.Value.Select(y => y.Card.Id).Contains(cardId));
        }

        public IEnumerable<IWillpowerfulInPlay> GetAllCharactersCommittedToQuest()
        {
            var all = new List<IWillpowerfulInPlay>();

            foreach (var pair in committedToQuest)
            {
                all.AddRange(pair.Value.ToList());
            }

            return all;
        }

        public IEnumerable<IWillpowerfulInPlay> GetCharactersCommitedToTheQuest(Guid playerId)
        {
            if (!committedToQuest.ContainsKey(playerId))
                return Enumerable.Empty<IWillpowerfulInPlay>();

            return committedToQuest[playerId].ToList();
        }

        public void CommitCharacterToQuest(IWillpowerfulInPlay character)
        {
            if (character == null)
                throw new ArgumentNullException("character");

            var player = character.GetController(Game);
            if (player == null)
                return;

            if (!committedToQuest.ContainsKey(player.StateId))
                committedToQuest.Add(player.StateId, new List<IWillpowerfulInPlay> { character });
            else
                committedToQuest[player.StateId].Add(character);
        }

        public void RemoveCharacterFromQuest(IWillpowerfulInPlay character)
        {
            if (character == null)
                throw new ArgumentNullException("character");

            foreach (var pair in committedToQuest)
            {
                var match = pair.Value.Where(x => x.Card.Id == character.Card.Id).FirstOrDefault();
                if (match != null)
                {
                    pair.Value.Remove(character);
                    return;
                }
            }
        }
    }
}
