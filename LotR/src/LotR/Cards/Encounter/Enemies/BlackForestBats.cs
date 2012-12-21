using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
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

            private void RemoveCharacterFromQuest(IGame game, IEffectHandle handle, IWillpowerfulInPlay character, IPlayer player, IQuestPhase questPhase)
            {
                questPhase.RemoveCharacterFromQuest(character);

                handle.Resolve(string.Format("{0} chose to remove '{1}' from the quest because of '{2}'", player.Name, character.Title, CardSource.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return new EffectHandle(this);

                var builder =
                    new ChoiceBuilder("Each player must choose 1 character currently commited to the quest", game, game.FirstPlayer);

                foreach (var player in game.Players)
                {
                    var characters = questPhase.GetCharactersCommitedToTheQuest(player.StateId);
                    var count = characters.Count();
                    if (count == 0)
                        continue;
                    else if (count == 1)
                    {
                        var first = characters.First();
                        builder.Question(string.Format("{0}, '{1}' is your only character committed to the quest. You must remove them from the quest", player.Name, first.Title))
                            .Answer(string.Format("Ok, remove '{0}' from the quest", first.Title), first, (source, handle, character) => RemoveCharacterFromQuest(source, handle, character, player, questPhase));
                    }
                    else
                    {
                        builder.Question(string.Format("{0}, which character do you want to remove from the quest?", player.Name))
                            .Answers(characters, (item) => string.Format("{0} ({1} willpower)", item.Title, item.Willpower), (source, handle, character) => RemoveCharacterFromQuest(source, handle, character, player, questPhase));
                    }
                }

                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
