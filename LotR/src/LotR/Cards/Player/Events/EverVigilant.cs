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
            : ActionCardEffectBase
        {
            public ReadyOneAlly(EverVigilant source)
                : base("Choose and ready 1 ally card.", source)
            {
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var card = Source as IPlayerCard;
                if (card == null)
                    return new EffectOptions();

                return new EffectOptions(new ChooseAlly(Source, card.Owner));
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var allyChoice = options.Choice as IChooseAlly;
                if (allyChoice == null || allyChoice.Ally == null)
                    return GetCancelledString();

                var exhaustable = allyChoice.Ally as IExhaustableInPlay;
                if (exhaustable == null)
                    return GetCancelledString();

                if (!exhaustable.IsExhausted)
                    return GetCancelledString();

                exhaustable.Ready();

                return ToString();
            }
        }
    }
}
