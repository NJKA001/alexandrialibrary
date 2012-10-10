using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.States.Areas;

namespace LotR.States
{
    public interface IGameState
        : IState
    {
        TArea GetArea<TArea>() where TArea : IArea;

        void AddEffect(IEffect effect);
    }
}
