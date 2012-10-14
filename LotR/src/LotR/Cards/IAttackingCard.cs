using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Cards
{
    public interface IAttackingCard
        : ICard
    {
        byte PrintedAttack { get; }

        void DetermineAttack(IDetermineAttack state);
    }
}
