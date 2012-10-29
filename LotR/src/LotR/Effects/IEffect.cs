using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Phases;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects
{
    public interface IEffect
    {
        Guid EffectId { get; }
        string Description { get; }

        IChoice GetChoice(IGame game);
        ICost GetCost(IGame game);
        ILimit GetLimit(IGame game);

        bool PaymentAccepted(IGame game, IPayment payment, IChoice choice);
        void Resolve(IGame game, IPayment payment, IChoice choice);
        string GetResolutionDescription(IGame game, IPayment payment, IChoice choice);
    }
}
