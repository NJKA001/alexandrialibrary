using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Phases;
using LotR.Effects.Payments;

namespace LotR.Effects
{
    public interface IEffect
    {
        Guid EffectId { get; }
        string Description { get; }

        IChoice GetChoice(IPhaseStep step);
        ICost GetCost(IPhaseStep step);
        ILimit GetLimit(IPhaseStep step);

        bool PaymentAccepted(IPhaseStep step, IPayment payment);
        void Resolve(IPhaseStep step, IChoice choice);
    }
}
