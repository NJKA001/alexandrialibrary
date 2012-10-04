using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects.Modifiers;

namespace LotR.Core
{
    public interface IDamageableInPlay
        : ICardInPlay
    {
        new IDamageableCard Card { get; }
        byte Damage { get; }

        void AddDamage(byte value);
        void RemoveDamage(byte value);

        IEnumerable<IHitPointModifier> HitPointModifiers { get; }
        void AddHitPointModifier(IHitPointModifier modifier);
        void RemoveHitPointModifier(IHitPointModifier modifier);
    }
}
