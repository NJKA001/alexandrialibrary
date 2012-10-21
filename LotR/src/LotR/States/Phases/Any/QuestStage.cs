using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Quests;
using LotR.Effects;

namespace LotR.States.Phases.Any
{
    public class QuestStage
        : StateBase, IQuestStage
    {
        public QuestStage(IGame game, IQuestInPlay currentStage, IQuestCard previousStage, IQuestCard nextStage)
            : base(game)
        {
            this.Game = game;
            this.CurrentStage = currentStage;
            this.PreviousStage = previousStage;
            this.nextStage = nextStage;
        }

        private IQuestCard nextStage;
        private bool stageIsDefeated;

        public IGame Game
        {
            get;
            private set;
        }

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

        public bool StageIsDefeated
        {
            get { return stageIsDefeated; }
            set
            {
                if (stageIsDefeated == value)
                    return;

                stageIsDefeated = value;
                OnPropertyChanged("StageIsDefeated");
            }
        }

        public void AddEffect(IEffect effect)
        {
            Game.AddEffect(effect);
        }
    }
}
