using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Any
{
    public interface ICheckForTraitStep
        : IPhaseStep
    {
        ICardInPlay CardInPlay { get; }
        Traits Trait { get; }
        bool HasTrait { get; set; }
    }
}
