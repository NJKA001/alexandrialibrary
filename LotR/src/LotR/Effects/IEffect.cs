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
        string Name { get; }
        string Description { get; }
        ISource Source { get; }

        IEffectOptions GetOptions(IGame game);

        bool CanBeTriggered(IGame game);
        bool PaymentAccepted(IGame game, IEffectOptions options);
        string Resolve(IGame game, IEffectOptions options);
    }
}
