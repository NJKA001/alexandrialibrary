using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;

namespace LotR.States.Controllers
{
    public class GameController
        : IGameController
    {
        private readonly IList<Action<IEffect, IChoice>> choiceOfferedCallbacks = new List<Action<IEffect, IChoice>>();
        private readonly IList<Func<IEffect, ICost, IPayment>> getPaymentCallbacks = new List<Func<IEffect, ICost, IPayment>>();
        private readonly IList<Action<IEffect>> effectAddedCallbacks = new List<Action<IEffect>>();
        private readonly IList<Action<IEffect, IEffectOptions, string>> effectResolvedCallbacks = new List<Action<IEffect, IEffectOptions, string>>();
        private readonly IList<Action<IEffect, IEffectOptions>> paymentRejectedCallbacks = new List<Action<IEffect, IEffectOptions>>();

        public void RegisterChoiceOfferedCallback(Action<IEffect, IChoice> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            choiceOfferedCallbacks.Add(callback);
        }

        public void RegisterEffectAddedCallback(Action<IEffect> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            effectAddedCallbacks.Add(callback);
        }

        public void RegisterEffectResolvedCallback(Action<IEffect, IEffectOptions, string> callback)
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

        public void RegisterPaymentRejectedCallback(Action<IEffect, IEffectOptions> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            paymentRejectedCallbacks.Add(callback);
        }

        public void ChoiceOffered(IEffect effect, IChoice choice)
        {
            foreach (var callback in choiceOfferedCallbacks)
                callback(effect, choice);
        }

        public void EffectAdded(IEffect effect)
        {
            foreach (var callback in effectAddedCallbacks)
                callback(effect);
        }

        public void EffectResolved(IEffect effect, IEffectOptions options, string status)
        {
            foreach (var callback in effectResolvedCallbacks)
                callback(effect, options, status);
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

        public void PaymentRejected(IEffect effect, IEffectOptions options)
        {
            foreach (var callback in paymentRejectedCallbacks)
                callback(effect, options);
        }
    }
}
