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
    public class AForkInTheRoad
        : QuestCardBase
    {
        public AForkInTheRoad()
            : base("A Fork in the Road", CardSet.Core, 120, ScenarioCode.Passage_Through_Mirkwood, new List<EncounterSet> { EncounterSet.Passage_Through_Mirkwood, EncounterSet.Dol_Guldur_Orcs, EncounterSet.Spiders_of_Mirkwood }, 2, 2, 0)
        {
        }

        private class ForcedChooseStageThreeRandomly
            : ForcedCardEffectBase, IAfterStageDefeated
        {
            public ForcedChooseStageThreeRandomly(AForkInTheRoad source)
                : base("When you defeat this stage, proceed to one of the 2 \"A Chosen Path\" stages, at random.", source)
            {
            }

            public void AfterStageDefeated(ICurrentQuestStage state)
            {
                state.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var questArea = game.GetStates<IQuestArea>().FirstOrDefault();
                if (questArea == null)
                    return;

                var currentStage = game.GetStates<ICurrentQuestStage>().FirstOrDefault();
                if (currentStage == null)
                    return;

                var random = new Random();
                var number = random.Next(1, 2);

                if (number == 1)
                {
                    var dontLeaveThePath = questArea.QuestDeck.Cards.Where(x => x.Title == "Don't Leave The Path").FirstOrDefault();
                    if (dontLeaveThePath != null)
                    {
                        currentStage.NextStage = dontLeaveThePath;
                    }
                }
                else
                {
                    var beornsPath = questArea.QuestDeck.Cards.Where(x => x.Title == "Beorn's Path").FirstOrDefault();
                    if (beornsPath != null)
                    {
                        currentStage.NextStage = beornsPath;
                    }
                }
            }
        }
    }
}
