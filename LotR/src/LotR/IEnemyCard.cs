using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IEnemyCard
        : IEncounterCard, IThreateningCard, IDamageableCard
    {
        byte EngagementCost { get; }
    }
}
