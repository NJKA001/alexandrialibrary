using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public interface IChooseEnemyToAttack
        : IState
    {
        IEnumerable<IAttackingInPlay> Attackers { get; }
        IEnumerable<IEnemyInPlay> Enemies { get; }
        IEnemyInPlay Choice { get; set; }

        void AddEnemy(IEnemyInPlay enemy);
        void RemoveEnemy(IEnemyInPlay enemy);
    }
}
