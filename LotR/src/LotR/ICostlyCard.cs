using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface ICostlyCard
        : IPlayerCard
    {
        Sphere BaseResourceSphere { get; }
        byte BaseResourceCost { get; }
        ICost GetResourceCost(IPhaseStep step);
    }
}
