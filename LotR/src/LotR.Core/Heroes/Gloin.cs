using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects;
using LotR.Core.Effects.CharacterAbilities;
using LotR.Core.Phases.Combat;

namespace LotR.Core.Heroes
{
    public class Gloin
        : HeroCardBase
    {
        public Gloin()
            : base("Gloin", Sphere.Leadership)
        {
            Trait(Traits.Dwarf);
            Trait(Traits.Noble);

            Effect(new AddResourcesWhenTakingDamage(this));
        }

        #region Abilities

        public class AddResourcesWhenTakingDamage
            : ResponseCharacterAbilityBase, IAfterDamageDealt
        {
            public AddResourcesWhenTakingDamage(Gloin source)
                : base("After Gloin suffers damage, add 1 resource to his resource pool for each point of damage he just suffered.", source)
            {
            }

            public void AfterDamageDealtSetup(IDealDamageStep step)
            {
                if (step.Target.CardId != Source.Id)
                    return;

                step.AddEffect(this);
            }

            public void AfterDamageDealtResolve(IDealDamageStep step)
            {
                step.AddEffect(new AddResources(step, new Dictionary<Guid, byte> { { Source.Id, step.Damage } }));
            }
        }

        #endregion
    }
}
