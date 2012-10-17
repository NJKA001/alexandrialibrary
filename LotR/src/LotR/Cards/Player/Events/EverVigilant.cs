using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Cards.Player.Events
{
    public class EverVigilant
        : EventCardBase
    {
        public EverVigilant()
            : base("Ever Vigilant", CardSet.Core, 20, Sphere.Leadership, 1)
        {
        }

        public class ReadyOneAlly
            : ActionEffectBase
        {
            public ReadyOneAlly(EverVigilant source)
                : base("Choose and ready 1 ally card.", source)
            {
            }

            public override IChoice GetChoice(IGame game)
            {
                return new ChooseAlly(Source, game.ActivePlayer);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var allyChoice = choice as IChooseAlly;
                if (allyChoice == null || allyChoice.Ally == null)
                    return;

                var exhaustable = allyChoice.Ally as IExhaustableInPlay;
                if (exhaustable == null)
                    return;

                if (!exhaustable.IsExhausted)
                    return;

                exhaustable.Ready();
            }
        }
    }
}
