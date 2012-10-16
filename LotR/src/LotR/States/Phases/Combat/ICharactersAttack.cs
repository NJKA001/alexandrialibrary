using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public interface ICharactersAttack
    {
        IPlayer AttackingPlayer { get; }
        IEnemyInPlay Enemy { get; }
        IEnumerable<IAttackingInPlay> Attackers { get; }

        void AddAttacker(IAttackingInPlay attacker);
        void RemoveAttacker(IAttackingInPlay attacker);
    }
}
