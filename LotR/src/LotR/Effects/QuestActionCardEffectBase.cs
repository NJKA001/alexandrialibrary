using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects
{
    public abstract class QuestActionCardEffectBase
        : ActionCardEffectBase, IQuestActionCardEffect
    {
        protected QuestActionCardEffectBase(string description, ICard cardSource)
            : base(description, cardSource)
        {
        }

        public override bool CanBeTriggered(IGame game)
        {
            switch (game.CurrentPhase.StepCode)
            {
                case PhaseStep.Quest_Player_Actions_Before_Commit_Characters:
                case PhaseStep.Quest_Player_Actions_Before_Staging:
                case PhaseStep.Quest_Player_Actions_Before_Quest_Resolution:
                case PhaseStep.Quest_Player_Actions_Before_End:
                    return true;
                default:
                    return false;
            }
        }

        public override string ToString()
        {
            return string.Format("Quest Action: {0}", Description);
        }
    }
}
