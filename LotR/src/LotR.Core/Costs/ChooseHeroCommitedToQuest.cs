using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Payments;
using LotR.Core.Phases.Quest;

namespace LotR.Core.Costs
{
    public class ChooseHeroCommitedToTheQuest
        : CostBase, ICost
    {
        public ChooseHeroCommitedToTheQuest(ICard source, ICommitToQuestStep step)
            : base("Choose a hero committed to the quest", source)
        {
            this.step = step;
        }

        private ICommitToQuestStep step;

        public override bool IsMetBy(IPayment payment)
        {
            if (payment == null)
                return false;

            var choice = payment as IChooseCharacterPayment;
            if (choice == null)
                return false;

            var hero = choice.Character as IHeroInPlay;
            if (hero == null)
                return false;

            if (!step.CommitedCharacters.Contains(hero))
                return false;

            return true;
        }
    }

}
