using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Payments;
using LotR.Core.Phases.Any;

namespace LotR.Core.Costs
{
    public class ChooseRevealedEnemy
        : CostBase
    {
        public ChooseRevealedEnemy(ICard source, IEncounterCardRevealedStep step)
            : base("Choose an enemy revealed from the encounter deck", source)
        {
            if (step == null)
                throw new ArgumentNullException("step");

            this.step = step;
        }

        private readonly IEncounterCardRevealedStep step;

        public override bool IsMetBy(IPayment payment)
        {
            if (payment == null)
                return false;

            var choice = payment as IChooseEnemyPayment;
            if (choice == null)
                return false;

            if (step.CardInPlay.CardId != choice.Enemy.CardId)
                return false;

            return true;
        }
    }
}
