using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Modifiers;
using LotR.Phases.Any;
using LotR.Phases.Combat;

namespace LotR.Heroes
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
                step.AddEffect(this);
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                var attackStep = step as IDetermineAttackStep;
                if (attackStep == null)
                    return;

                var damageable = step.GetCardInPlay(Source.Id) as IDamageableInPlay;
                if (damageable != null)
                {
                    attackStep.Attack += damageable.Damage;
                }
            }
        }

        #endregion
    }
}
