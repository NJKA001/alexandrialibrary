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

        IChoice GetChoice(IGameState state);
        ICost GetCost(IGameState state);
        ILimit GetLimit(IGameState state);

        bool PaymentAccepted(IGameState state, IPayment payment, IChoice choice);
        void Resolve(IGameState state, IPayment payment, IChoice choice);
    }
}
