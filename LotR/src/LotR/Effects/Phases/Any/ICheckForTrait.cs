using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Phases.Any
{
    public interface ICheckForTrait
    {
        void CheckForTrait(ICheckForTraitStep step);
    }
}
