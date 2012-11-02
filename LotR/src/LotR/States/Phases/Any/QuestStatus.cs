using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Quests;
using LotR.Effects;

namespace LotR.States.Phases.Any
{
    public class QuestStatus
        : StateBase, IQuestStatus
    {
        public QuestStatus(IGame game, IQuestInPlay currentStage, IQuestCard previousStage, IQuestCard nextStage)
            : base(game)
        {
            this.CurrentStage = currentStage;
            this.PreviousStage = previousStage;
            this.nextStage = nextStage;
        }

        private IQuestCard nextStage;
        private bool isStageDefeated;

        public IQuestInPlay CurrentStage
        {
            get;
            private set;
        }

        public IQuestCard PreviousStage
        {
            get;
            private set;
        }

        public IQuestCard NextStage
        {
            get { return nextStage; }
            set
            {
                if (nextStage == value)
                    return;

                nextStage = value;
                OnPropertyChanged("NextStage");
            }
        }

        public bool IsStageDefeated
        {
            get { return isStageDefeated; }
            set
            {
                if (isStageDefeated == value)
                    return;

                isStageDefeated = value;
                OnPropertyChanged("IsStageDefeated");
            }
        }
    }
}
