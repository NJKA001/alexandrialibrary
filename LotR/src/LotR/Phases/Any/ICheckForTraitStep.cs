using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    public interface ICheckForTraitStep
        : IPhaseStep
    {
        ICardInPlay CardInPlay { get; }
        Trait Trait { get; }
        bool HasTrait { get; set; }
    }
}
