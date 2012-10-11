using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Phases.Resource
{
    public interface IDrawCardsStep
    {
        bool CanDrawCards { get; set; }
        byte NumberOfCardsToDraw { get; set; }
    }
}
