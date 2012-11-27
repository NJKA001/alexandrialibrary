using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Costs;
using LotR.Effects.Payments;

namespace LotR.States.Controllers
{
    public interface IGameController
    {
        void RegisterEffectAddedCallback(Action<IEffect> callback);
        void RegisterEffectCancelledCallback(Action<IEffect, IEffectHandle> callback);
        void RegisterEffectResolvedCallback(Action<IEffect, IEffectHandle> callback);
        void RegisterGetPaymentCallback(Func<IEffect, ICost, IPayment> callback);
        void RegisterOfferChoiceCallback(Action<IEffect, IChoice> callback);
        void RegisterPaymentAcceptedCallback(Action<IEffect, IEffectHandle> callback);
        void RegisterPaymentRejectedCallback(Action<IEffect, IEffectHandle> callback);

        void EffectAdded(IEffect effect);
        void EffectCancelled(IEffect effect, IEffectHandle handle);
        void EffectResolved(IEffect effect, IEffectHandle handle);
        IPayment GetPayment(IEffect effect, ICost cost);
        void OfferChoice(IEffect effect, IChoice choice);
        void PaymentAccepted(IEffect effect, IEffectHandle handle);
        void PaymentRejected(IEffect effect, IEffectHandle handle);
    }
}
