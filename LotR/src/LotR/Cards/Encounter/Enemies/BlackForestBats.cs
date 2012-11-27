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

            private void RemoveCharacterFromQuest(IGame game, IEffectHandle handle, IWillpowerfulInPlay character)
            {
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
                }



                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
