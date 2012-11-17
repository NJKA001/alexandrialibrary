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

            public override IEffectHandle GetHandle(IGame game)
            {
                var card = source as IPlayerCard;
                if (card == null)
                    return new EffectHandle(this);

                return new EffectHandle(this, new ChooseAlly(source, card.Owner));
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var allyChoice = handle.Choice as IChooseAlly;
                if (allyChoice == null || allyChoice.Ally == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var exhaustable = allyChoice.Ally as IExhaustableInPlay;
                if (exhaustable == null)
                    { handle.Cancel(GetCancelledString()); return; }

                if (!exhaustable.IsExhausted)
                    { handle.Cancel(GetCancelledString()); return; }

                exhaustable.Ready();

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
