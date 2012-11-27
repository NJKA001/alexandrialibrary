using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Costs;
using LotR.Effects.Payments;

namespace LotR.States.Controllers
{
    public class GameController
        : IGameController
    {
        private readonly IList<Func<IEffect, ICost, IPayment>> getPaymentCallbacks = new List<Func<IEffect, ICost, IPayment>>();
        private readonly IList<Action<IEffect>> effectAddedCallbacks = new List<Action<IEffect>>();
        private readonly IList<Action<IEffect, IEffectHandle>> effectCancelledCallbacks = new List<Action<IEffect, IEffectHandle>>();
        private readonly IList<Action<IEffect, IEffectHandle>> effectResolvedCallbacks = new List<Action<IEffect, IEffectHandle>>();
        private readonly IList<Action<IEffect, IChoice>> offerChoiceCallbacks = new List<Action<IEffect, IChoice>>();
        private readonly IList<Action<IEffect, IEffectHandle>> paymentAcceptedCallbacks = new List<Action<IEffect, IEffectHandle>>();
        private readonly IList<Action<IEffect, IEffectHandle>> paymentRejectedCallbacks = new List<Action<IEffect, IEffectHandle>>();

        public void RegisterEffectAddedCallback(Action<IEffect> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            effectAddedCallbacks.Add(callback);
        }

        public void RegisterEffectCancelledCallback(Action<IEffect, IEffectHandle> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            effectCancelledCallbacks.Add(callback);
        }

        public void RegisterEffectResolvedCallback(Action<IEffect, IEffectHandle> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            effectResolvedCallbacks.Add(callback);
        }

        public void RegisterGetPaymentCallback(Func<IEffect, ICost, IPayment> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            getPaymentCallbacks.Add(callback);
        }

        public void RegisterOfferChoiceCallback(Action<IEffect, IChoice> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            offerChoiceCallbacks.Add(callback);
        }

        public void RegisterPaymentAcceptedCallback(Action<IEffect, IEffectHandle> callback)
        {
        }

        public void RegisterPaymentRejectedCallback(Action<IEffect, IEffectHandle> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            paymentRejectedCallbacks.Add(callback);
        }

        public void EffectAdded(IEffect effect)
        {
            foreach (var callback in effectAddedCallbacks)
                callback(effect);
        }

        public void EffectCancelled(IEffect effect, IEffectHandle handle)
        {
            foreach (var callback in effectCancelledCallbacks)
                callback(effect, handle);
        }

        public void EffectResolved(IEffect effect, IEffectHandle handle)
        {
            foreach (var callback in effectResolvedCallbacks)
                callback(effect, handle);
        }

        public IPayment GetPayment(IEffect effect, ICost cost)
        {
            IPayment payment = null;
            foreach (var callback in getPaymentCallbacks)
            {
                payment = callback(effect, cost);
                if (payment != null)
                {
                    return payment;
                }
            }
            return payment;
        }

        public void OfferChoice(IEffect effect, IChoice choice)
        {
            foreach (var callback in offerChoiceCallbacks)
                callback(effect, choice);
        }

        public void PaymentAccepted(IEffect effect, IEffectHandle handle)
        {
            foreach (var callback in paymentAcceptedCallbacks)
                callback(effect, handle);
        }

        public void PaymentRejected(IEffect effect, IEffectHandle handle)
        {
            foreach (var callback in paymentRejectedCallbacks)
                callback(effect, handle);
        }
    }
}
