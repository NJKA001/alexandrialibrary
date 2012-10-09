using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Modifiers;

namespace LotR.Games.Phases.Combat
{
    public interface ICharactersAttackStep
        : IPhaseStep
    {
        IEnemyInPlay Target { get; }
        IEnumerable<IAttackingCard> Attackers { get; }
        IEnumerable<IAttackModifier> AttackModifiers { get; }
        IEnumerable<IDefenseModifier> DefenseModifiers { get; }

        void AddAttacker(IAttackingCard attacker);
        void RemoveAttacker(IAttackingCard attacker);

        void AddAttackModifier(IAttackModifier modifier);
        void RemoveAttackModifier(IAttackModifier modifier);

        void AddDefenseModifier(IDefenseModifier modifier);
        void RemoveDefenseModifier(IDefenseModifier modifier);
    }
}
