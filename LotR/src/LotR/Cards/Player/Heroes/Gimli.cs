using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Modifiers;
using LotR.States;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Combat;

namespace LotR.Cards.Player.Heroes
{
    public class Gimli
        : HeroCardBase
    {
        public Gimli()
            : base("Gimli", CardSet.Core, 4, Sphere.Tactics, 11, 2, 2, 2, 5)
        {
            AddTrait(Trait.Dwarf);
            AddTrait(Trait.Noble);
            AddTrait(Trait.Warrior);

            AddEffect(new StrengthBonusForDamage(this));
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
                //var attackStep = step as IDetermineAttackStep;
                //if (attackStep == null)
                //    return;

                //var damageable = step.GetCardInPlay(Source.Id) as IDamageableInPlay;
                //if (damageable != null)
                //{
                //    attackStep.Attack += damageable.Damage;
                //}
            }
        }

        #endregion
    }
}
