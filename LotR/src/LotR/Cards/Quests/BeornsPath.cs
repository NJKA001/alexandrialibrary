using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;

namespace LotR.Cards.Quests
{
    public class BeornsPath
        : QuestCardBase
    {
        public BeornsPath()
            : base("Beorn's Path", CardSet.Core, 122, ScenarioCode.Passage_Through_Mirkwood, new List<EncounterSet> { EncounterSet.Passage_Through_Mirkwood, EncounterSet.Dol_Guldur_Orcs, EncounterSet.Spiders_of_Mirkwood }, 3, 10, 0)
        {
            AddEffect(new PassiveCannotBeDefeatedWhileUngoliantsSpawnInPlay(this));
        }

        private class PassiveCannotBeDefeatedWhileUngoliantsSpawnInPlay
            : PassiveCardEffectBase, IBeforeStageDefeated
        {
            public PassiveCannotBeDefeatedWhileUngoliantsSpawnInPlay(BeornsPath source)
                : base("Players cannot defeat this stage while Ungoliant's Spawn is in play. If players defeat this stage, they have won the game.", source)
            {
            }

            public void BeforeStageDefeated(ICurrentQuestStage state)
            {
                var game = state.GetStates<IGame>().FirstOrDefault();
                if (game == null)
                    return;

                var stagingArea = game.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return;

                var ungoliantsSpawn = stagingArea.CardsInStagingArea.Where(x => x.Title == "Ungoliant's Spawn").FirstOrDefault() as IEnemyInPlay;
                if (ungoliantsSpawn == null)
                    return;

                state.StageIsDefeated = false;
            }
        }
    }
}
