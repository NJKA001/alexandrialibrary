using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Phases.Combat
{
    public interface IChooseAttackersStep
    {
        IPlayer Player { get; }
        IEnumerable<IAttackingCard> Attackers { get; }

        void AddAttacker(IAttackingCard attacker);
        void RemoveAttacker(IAttackingCard attacker);
    }
}
