using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Payments;

namespace LotR.States.Controllers
{
    public interface IGameController
    {
        void RegisterEffectAddedCallback(Action<IEffect> callback);
        void RegisterEffectCancelledCallback(Action<IEffect, IEffectHandle> callback);
        void RegisterEffectResolvedCallback(Action<IEffect, IEffectHandle> callback);
        void RegisterOfferChoiceCallback(Action<IEffect, IChoice> callback);
        
        void EffectAdded(IEffect effect);
        void EffectCancelled(IEffect effect, IEffectHandle handle);
        void EffectResolved(IEffect effect, IEffectHandle handle);
        void OfferChoice(IEffect effect, IChoice choice);
    }
}
