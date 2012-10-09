using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player.Heroes;
using LotR.Effects.Payments;

namespace LotR.Effects.Costs
{
    public class ChooseHero
        : CostBase
    {
        public ChooseHero(ISource source)
            : base("Choose a hero", source)
        {
        }

        public override bool IsMetBy(IPayment payment)
        {
            if (payment == null)
                return false;

            var choice = payment as IChooseCharacterPayment;
            if (choice == null)
                return false;

            var hero = choice.Character as IHeroCard;
            if (hero == null)
                return false;

            return true;
        }
    }
}
