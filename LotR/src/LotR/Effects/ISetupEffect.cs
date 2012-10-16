using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public interface ISetupEffect
        : IPassiveEffect
    {
        void Setup(IGameState state);
    }
}
