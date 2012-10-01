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
            : base("Gimli", SetNames.Core, 4, Sphere.Tactics, 11, 2, 2, 2, 5)
        {
            Trait(Traits.Dwarf);
            Trait(Traits.Noble);
            Trait(Traits.Warrior);

            Effect(new StrengthBonusForDamage(this));
        }

        #region Abilities

        public class StrengthBonusForDamage
            : PassiveCharacterAbilityBase, IDetermineAttack
        {
            public StrengthBonusForDamage(Gimli source)
                : base("Gimli gets +1 attack for each damage token on him.", source)
            {
            }

            public void DetermineAttack(IDetermineAttackStep step)
            {
                var damageable = step.GetCardInPlay(Source.Id) as IDamageableInPlay;
                if (damageable == null)
                {
                    step.Attack = 0;
                }
                else
                {
                    step.Attack = (byte)(step.Attack + damageable.Damage);
                }
            }
        }

        #endregion
    }
}
