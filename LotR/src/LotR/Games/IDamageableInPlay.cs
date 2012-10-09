using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Modifiers;

namespace LotR.Games
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
