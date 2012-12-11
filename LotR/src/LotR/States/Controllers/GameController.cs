using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Payments;

namespace LotR.States.Controllers
{
    public class GameController
        : IGameController
    {
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

        public void RegisterOfferChoiceCallback(Action<IEffect, IChoice> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            offerChoiceCallbacks.Add(callback);
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

        public void OfferChoice(IEffect effect, IChoice choice)
        {
            foreach (var callback in offerChoiceCallbacks)
                callback(effect, choice);
        }
    }
}
