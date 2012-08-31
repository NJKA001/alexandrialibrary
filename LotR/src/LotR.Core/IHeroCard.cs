using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IHeroCard
        : IPlayerCard
    {
        byte ThreatCost { get; }
        IEnumerable<Sphere> ResourceIcons { get; }
    }
}
