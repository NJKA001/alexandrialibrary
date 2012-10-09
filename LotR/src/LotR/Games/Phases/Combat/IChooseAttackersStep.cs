using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Games.Phases.Combat
{
    public interface IChooseAttackersStep
    {
        IPlayer Player { get; }
        IEnumerable<IAttackingCard> Attackers { get; }

        void AddAttacker(IAttackingCard attacker);
        void RemoveAttacker(IAttackingCard attacker);
    }
}
