using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;

namespace LotR.Effects.Costs
{
    public interface IResourceCost
        : ICost
    {
        ICharacterInPlay Character { get; }
        byte Resources { get; }
    }
}
