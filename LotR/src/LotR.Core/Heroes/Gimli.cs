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
        : HeroCardBase, IAfterDamageDealt, IAfterDamageHealed
    {
        public Gimli()
            : base("Gimli", Sphere.Tactics)
        {
            Trait(Traits.Dwarf);
            Trait(Traits.Noble);
            Trait(Traits.Warrior);


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
                : base(startPhase, source, Duration.Permanent, value)
            {
            }
        }

        #region IAfterDamageDealt

        public void AfterDamageDealtSetup(IDealDamageStep step)
        {
        }

        public void AfterDamageDealtResolve(IDealDamageStep step)
        {
            var hero = step.GetCardInPlay(this.Id) as IHeroInPlay;
            if (hero == null)
                return;

            var modifier = hero.Modifiers.OfType<GimliAttackModifier>().FirstOrDefault();
            
            if (modifier == null || hero.Damage != modifier.Value)
            {
                if (modifier != null)
                    hero.RemoveModifier(modifier);

                if (hero.Damage == 0)
                    return;

                hero.AddModifier(new GimliAttackModifier(step.Phase, this, hero.Damage));
            }
        }

        #endregion

        #region IAfterDamageHealed

        public void AfterDamageHealedSetup(IHealDamageStep step)
        {
        }

        public void AfterDamageHealedResolve(IHealDamageStep step, IPayment payment)
        {
            var hero = step.GetCardInPlay(this.Id) as IHeroInPlay;
            if (hero == null)
                return;

            var modifier = hero.Modifiers.OfType<GimliAttackModifier>().FirstOrDefault();

            if (modifier == null || hero.Damage != modifier.Value)
            {
                if (modifier != null)
                    hero.RemoveModifier(modifier);

                if (hero.Damage == 0)
                    return;

                hero.AddModifier(new GimliAttackModifier(step.Phase, this, hero.Damage));
            }
        }

        #endregion
    }
}
