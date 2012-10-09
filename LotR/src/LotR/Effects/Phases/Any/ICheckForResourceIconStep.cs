using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;

namespace LotR.Effects.Phases.Any
{
    public interface ICheckForResourceIconStep
        : IPhaseStep
    {
        Sphere ResourceIcon { get; }
        IResourcefulCard Source { get; }
        bool HasResourceIcon { get; set; }
    }
}
