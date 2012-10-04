using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Combat
{
    public interface IChooseAttackersStep
    {
        IPlayer Player { get; }
        IEnumerable<IAttackingCard> Attackers { get; }

        void AddAttacker(IAttackingCard attacker);
        void RemoveAttacker(IAttackingCard attacker);
    }
}
