using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IHeroCard
        : ICharacterCard, IResourcefulCard
    {
        byte ThreatCost { get; }
    }
}
