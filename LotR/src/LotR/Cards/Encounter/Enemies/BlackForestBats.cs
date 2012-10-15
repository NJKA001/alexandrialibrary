using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Phases.Quest;

namespace LotR.Cards.Encounter.Enemies
{
    public class BlackForestBats
        : EnemyCardBase
    {
        public BlackForestBats()
            : base("Black Forest Bats", CardSet.Core, 98, EncounterSet.Passage_Through_Mirkwood, 1, 1, 15, 1, 0, 2, 0)
        {
            AddTrait(Trait.Creature);

            AddEffect(new WhenRevealedEachPlayerRemovesOneCharacterFromQuest(this));
        }

        private class WhenRevealedEachPlayerRemovesOneCharacterFromQuest
            : WhenRevealedEffectBase
        {
            public WhenRevealedEachPlayerRemovesOneCharacterFromQuest(BlackForestBats source)
                : base("Each player must choose 1 character currently committed to a quest, and remove that character from the quest. (The chosen character does not ready.)", source)
            {
            }

            public override IChoice GetChoice(IGameState state)
            {
                var commitedToQuest = state.GetStates<ICharactersCommittedToQuest>().FirstOrDefault();
                if (commitedToQuest == null)
                    return null;

                var players = state.GetStates<IPlayer>();
                if (players.Count() == 0)
                    return null;

                var questingCharacters = new Dictionary<Guid, IList<IWillpowerfulCard>>();

                foreach (var player in players)
                {
                    var questingForPlayer = commitedToQuest.GetCharactersCommittedToQuest(player.StateId).Select(x => x.Card).ToList();
                    questingCharacters.Add(player.StateId, questingForPlayer);
                }

                if (questingCharacters.All(x => x.Value.Count == 0))
                    return null;

                return new PlayersChooseCards<IWillpowerfulCard>("Each player must choose 1 character currently commited to a quest", Source, players, 1, questingCharacters);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var commitedToQuest = state.GetStates<ICharactersCommittedToQuest>().FirstOrDefault();
                if (commitedToQuest == null)
                    return;

                var removeChoice = choice as IPlayersChooseCards<IWillpowerfulCard>;
                if (removeChoice == null)
                    return;

                foreach (var player in removeChoice.Players)
                {
                    var removeFromQuest = removeChoice.GetChosenCards(player.StateId).FirstOrDefault();
                    if (removeFromQuest == null)
                        continue;

                    var inPlay = state.GetState<IWillpowerfulInPlay>(removeFromQuest.Id);
                    if (inPlay == null)
                        continue;

                    commitedToQuest.RemoveCharacterFromQuest(inPlay);
                }
            }
        }
    }
}
