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
    public class AForkInTheRoad
        : PassageThroughMirkwoodQuestCardBase
    {
        public AForkInTheRoad()
            : base("A Fork in the Road", 120, 2, 2)
        {
        }

        private class ForcedChooseStageThreeRandomly
            : ForcedCardEffectBase, IAfterStageDefeated
        {
            public ForcedChooseStageThreeRandomly(AForkInTheRoad source)
                : base("When you defeat this stage, proceed to one of the 2 \"A Chosen Path\" stages, at random.", source)
            {
            }

            public void AfterStageDefeated(IQuestStatus stage)
            {
                stage.Game.AddEffect(this);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var currentStage = game.QuestArea.GetCurrentQuestStage();
                if (currentStage == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var random = new Random();
                var number = random.Next(1, 2);

                if (number == 1)
                {
                    var dontLeaveThePath = game.QuestArea.QuestDeck.Cards.Where(x => x.Title == "Don't Leave The Path").FirstOrDefault();
                    if (dontLeaveThePath != null)
                    {
                        currentStage.NextStage = dontLeaveThePath;
                    }
                }
                else
                {
                    var beornsPath = game.QuestArea.QuestDeck.Cards.Where(x => x.Title == "Beorn's Path").FirstOrDefault();
                    if (beornsPath != null)
                    {
                        currentStage.NextStage = beornsPath;
                    }
                }

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
