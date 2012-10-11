using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.States
{
    public interface IGameState
        : IState
    {
        Phase CurrentPhase { get; }

        void AddEffect(IEffect effect);
    }
}
