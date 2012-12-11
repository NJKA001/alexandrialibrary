using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects
{
    public interface IEffect
    {
        bool CanBeTriggered(IGame game);
        IEffectHandle GetHandle(IGame game);

        void Validate(IGame game, IEffectHandle handle);
        void Trigger(IGame game, IEffectHandle handle);
    }
}
