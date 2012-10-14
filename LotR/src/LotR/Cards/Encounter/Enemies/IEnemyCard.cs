using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Encounter.Enemies
{
    public interface IEnemyCard
        : IEncounterCard, 
        IThreateningCard, 
        IDamageableCard, 
        IAttackingCard, 
        IDefendingCard,
        ICanHaveAttachments,
        IVictoryCard
    {
        byte EngagementCost { get; }
    }
}
