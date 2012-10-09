using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Payments;
using LotR.Games;
using LotR.Effects.Phases.Quest;

namespace LotR.Effects.Costs
{
    public class ChooseHeroCommitedToTheQuest
        : CostBase, ICost
    {
        public ChooseHeroCommitedToTheQuest(ISource source, ICommitToQuestStep step)
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
