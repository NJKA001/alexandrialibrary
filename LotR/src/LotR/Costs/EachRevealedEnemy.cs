using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Payments;
using LotR.Phases.Any;

namespace LotR.Costs
{
    public class EachRevealedEnemy
        : CostBase
    {
        public EachRevealedEnemy(ICard source, IEncounterCardRevealedStep step)
            : base("Each time an enemy is revealed from the encounter deck", source)
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

            var revealed = step.Phase.Round.Game.StagingArea.RevealedEncounterCard;
            if (revealed == null)
                return false;

            if (revealed.Id != choice.Enemy.CardId)
                return false;

            return true;
        }
    }
}
