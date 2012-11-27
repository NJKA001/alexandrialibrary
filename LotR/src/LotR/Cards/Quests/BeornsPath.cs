using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;

namespace LotR.Cards.Quests
{
    public class BeornsPath
        : PassageThroughMirkwoodQuestCardBase
    {
        public BeornsPath()
            : base("Beorn's Path", 122, 3, 10)
        {
            AddEffect(new PassiveCannotBeDefeatedWhileUngoliantsSpawnInPlay(this));
        }

        private class PassiveCannotBeDefeatedWhileUngoliantsSpawnInPlay
            : PassiveCardEffectBase, IBeforeStageDefeated, IDuringCheckGameStatus
        {
            public PassiveCannotBeDefeatedWhileUngoliantsSpawnInPlay(BeornsPath source)
                : base("Players cannot defeat this stage while Ungoliant's Spawn is in play. If players defeat this stage, they have won the game.", source)
            {
            }

            public void BeforeStageDefeated(IQuestStatus questStatus)
            {
                var ungoliantsSpawn = questStatus.Game.StagingArea.CardsInStagingArea.Where(x => x.Title == "Ungoliant's Spawn").FirstOrDefault() as IEnemyInPlay;
                if (ungoliantsSpawn == null)
                    return;

                questStatus.IsStageDefeated = false;
            }

            public void DuringCheckGameStatus(IGameStatus gameStatus)
            {
                if (gameStatus.Game.QuestArea.ActiveQuest == null || gameStatus.Game.QuestArea.ActiveQuest.Card.Id != source.Id)
                    return;

                var ungoliantsSpawn = gameStatus.Game.StagingArea.CardsInStagingArea.Where(x => x.Title == "Ungoliant's Spawn").FirstOrDefault() as IEnemyInPlay;
                if (ungoliantsSpawn != null)
                {
                    gameStatus.IsPlayerVictory = false;
                }
                else if (gameStatus.Game.QuestArea.ActiveQuest.Progress >= gameStatus.Game.QuestArea.ActiveQuest.Card.QuestPoints)
                {
                    gameStatus.IsPlayerVictory = true;
                }
            }
        }
    }
}
