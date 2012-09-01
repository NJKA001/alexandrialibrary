using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IKillableInPlay
        : ICardInPlay
    {
        new IKillableCard Card { get; }
        byte Damage { get; }

        void AddDamage(byte value);
        void RemoveDamage(byte value);

        IEnumerable<IModifier> HitPointModifiers { get; }
        void AddHitPointModifier(IModifier modifier);
        void RemoveHitPointModifier(IModifier modifier);
    }
}
