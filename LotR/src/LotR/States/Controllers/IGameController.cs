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
    public interface IGameController
    {
        void RegisterChoiceOfferedCallback(Action<IEffect, IChoice> callback);
        void RegisterEffectAddedCallback(Action<IEffect> callback);
        void RegisterEffectResolvedCallback(Action<IEffect, IEffectOptions, string> callback);
        void RegisterGetPaymentCallback(Func<IEffect, ICost, IPayment> callback);
        void RegisterPaymentRejectedCallback(Action<IEffect, IEffectOptions> callback);

        void ChoiceOffered(IEffect effect, IChoice choice);
        void EffectAdded(IEffect effect);
        void EffectResolved(IEffect effect, IEffectOptions options, string status);
        IPayment GetPayment(IEffect effect, ICost cost);
        void PaymentRejected(IEffect effect, IEffectOptions options);
    }
}
