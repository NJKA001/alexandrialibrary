using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Costs;
using LotR.Core.Effects;
using LotR.Core.Payments;

namespace LotR.Core.Events
{
    public class EverVigilant
        : EventCardBase
    {
        public EverVigilant()
            : base("Ever Vigilant", SetNames.Core, 20, Sphere.Leadership, 1)
        {
        }

        public class ReadyOneAlly
            : ActionEffectBase
        {
            public ReadyOneAlly(EverVigilant source)
                : base("Choose and ready 1 ally card.", source)
            {
            }

            public override ICost GetCost(IPhaseStep step)
            {
                return new ChooseAlly(Source);
            }

            public override void Resolve(IPhaseStep step, IPayment payment)
            {
                if (payment == null)
                    return;

                var choice = payment as IChooseCharacterPayment;
                if (choice == null)
                    return;

                var ally = choice.Character as IAllyCard;
                if (ally == null)
                    return;

                var exhaustable = step.GetCardInPlay(ally.Id) as IExhaustableCard;
                if (exhaustable == null)
                    return;

                exhaustable.Ready();
            }
        }
    }
}
