using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Combat
{
    public interface IChooseEnemyToAttackStep
        : IPhaseStep
    {
        IEnumerable<IAttackingCard> Attackers { get; }
        IEnumerable<IEnemyInPlay> Enemies { get; }
        IEnemyInPlay Choice { get; set; }

        void AddEnemy(IEnemyInPlay enemy);
        void RemoveEnemy(IEnemyInPlay enemy);
    }
}
