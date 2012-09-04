using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IHeroCard
        : ICharacterCard, IResourcefulCard
    {
        byte ThreatCost { get; }
        IEnumerable<Sphere> ResourceIcons { get; }
    }
}
