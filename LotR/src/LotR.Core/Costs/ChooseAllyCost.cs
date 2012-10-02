using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Payments;

namespace LotR.Core.Costs
{
    public class ChooseAllyCost
        : CostBase
    {
        public ChooseAllyCost(ICard source)
            : base("Choose an ally.", source)
        {
        }

        public override bool IsMetBy(IPayment payment)
        {
            if (payment == null)
                return false;

            var choice = payment as IChooseCharacterPayment;
            if (choice == null)
                return false;

            var ally = choice.Character as IAllyCard;
            if (ally == null)
                return false;

            return true;
        }
    }
}
