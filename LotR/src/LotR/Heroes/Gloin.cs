﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.CharacterAbilities;
using LotR.Phases.Combat;

namespace LotR.Heroes
{
    public class Gloin
        : HeroCardBase, IAfterDamageDealtToCharacter
    {
        public Gloin()
            : base("Gloin", SetNames.Core, 3, Sphere.Leadership, 9, 2, 2, 1, 4)
        {
            Trait(Traits.Dwarf);
            Trait(Traits.Noble);
        }

        #region Abilities

        public class AddResourcesWhenTakingDamage
            : ResponseCharacterAbilityBase
        {
            public AddResourcesWhenTakingDamage(Gloin source, ICardInPlay permanent, byte value)
                : base("After Gloin suffers damage, add 1 resource to his resource pool for each point of damage he just suffered.", source)
            {
                this.permanent = permanent;
                this.value = value;
            }

            private readonly ICardInPlay permanent;
            private readonly byte value;

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                permanent.AddResources(value);
            }
        }

        #endregion

        public void AfterDamageDealtToCharacter(IDealDamageToCharacterStep step)
        {
            if (step.Target.CardId != this.Id)
                return;

            step.AddEffect(new AddResourcesWhenTakingDamage(this, step.Target, step.Damage));
        }
    }
}
