using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States.Areas;
using LotR.States.Phases.Any;

namespace LotR.Effects.Costs
{
    public class EachRevealedEnemy
        : CostBase
    {
        public EachRevealedEnemy(ISource source, IEncounterCardRevealed state)
            : base("Each time an enemy is revealed from the encounter deck", source)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            this.state = state;
        }

        private readonly IEncounterCardRevealed state;

        public override bool IsMetBy(IPayment payment)
        {
            if (payment == null)
                return false;

            var choice = payment as IChooseEnemyPayment;
            if (choice == null)
                return false;

            var stagingArea = state.GetStates<IStagingArea>().FirstOrDefault();
            if (stagingArea == null)
                return false;

            var revealed = stagingArea.RevealedEncounterCard;
            if (revealed == null)
                return false;

            if (revealed.Id != choice.Enemy.Card.Id)
                return false;

            return true;
        }
    }
}
