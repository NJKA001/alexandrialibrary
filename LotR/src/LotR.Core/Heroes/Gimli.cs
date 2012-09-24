using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects;
using LotR.Core.Effects.CharacterAbilities;
using LotR.Core.Effects.Modifiers;
using LotR.Core.Phases.Any;
using LotR.Core.Phases.Combat;

namespace LotR.Core.Heroes
{
    public class Gimli
        : HeroCardBase
    {
        public Gimli()
            : base("Gimli", Sphere.Tactics)
        {
            Trait(Traits.Dwarf);
            Trait(Traits.Noble);
            Trait(Traits.Warrior);

            Effect(new StrengthBonusForDamage(this));
        }

        #region Abilities

        public class StrengthBonusForDamage
            : PassiveCharacterAbilityBase
        {
            public StrengthBonusForDamage(Gimli source)
                : base("Gimli gets +1 attack for each damage token on him.", source)
            {
            }
        }

        #endregion

        public class GimliAttackModifier
            : AttackModifier
        {
            public GimliAttackModifier(IPhase startPhase, ICard source, int value)
                : base(startPhase, source, Duration.Immediate, value)
            {
            }
        }

        public override void DetermineAttack(IDetermineAttackStep step)
        {
            var damageable = step.GetCardInPlay(this.Id) as IDamageableInPlay;
            if (damageable == null)
            {
                step.Attack = 0;
            }
            else
            {
                step.Attack = (byte)(this.Attack + damageable.Damage);
            }
        }
    }
}
