using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;

namespace LotR.Effects.Phases.Any
{
    public interface ICharacterLeavesPlayStep
    {
        ICharacterInPlay Character { get; }
    }
}
