using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Effects.Phases.Resource
{
    public interface IDuringDrawingResourceCards
        : IEffect
    {
        void DuringDrawingResourceCards(IPlayersDrawingCards state);
    }
}
