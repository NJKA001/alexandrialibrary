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

            public override IEffectOptions GetOptions(IGame game)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return new EffectOptions();
                
                var questingCharacters = new Dictionary<Guid, IList<IWillpowerfulCard>>();

                foreach (var player in game.Players)
                {
                    var questingForPlayer = questPhase.GetCharactersCommitedToTheQuest(player.StateId).Select(x => x.Card).ToList();
                    questingCharacters.Add(player.StateId, questingForPlayer);
                }

                if (questingCharacters.All(x => x.Value.Count == 0))
                    return new EffectOptions();

                return new EffectOptions(new PlayersChooseCards<IWillpowerfulCard>("Each player must choose 1 character currently commited to a quest", source, game.Players, 1, questingCharacters));
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return GetCancelledString();

                var removeChoice = options.Choice as IPlayersChooseCards<IWillpowerfulCard>;
                if (removeChoice == null)
                    return GetCancelledString();

                foreach (var player in removeChoice.Players)
                {
                    var removeFromQuest = removeChoice.GetChosenCards(player.StateId).FirstOrDefault();
                    if (removeFromQuest == null)
                        continue;

                    var character = player.CardsInPlay.OfType<IWillpowerfulInPlay>().Where(x => x.Card.Id == removeFromQuest.Id).FirstOrDefault();
                    if (character == null)
                        continue;

                    questPhase.RemoveCharacterFromQuest(character);
                }

                return ToString();
            }
        }
    }
}
