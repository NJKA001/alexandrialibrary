using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Encounter
{
    public interface IEnemiesEngaged
        : IState
    {
        IPlayer Player { get; }
        IEnumerable<IEnemyInPlay> Enemies { get; }

        void AddEnemy(IEnemyInPlay enemy);
        void RemoveEnemy(IEnemyInPlay enemy);
    }
}
