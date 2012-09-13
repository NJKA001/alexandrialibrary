using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects;
using LotR.Core.Effects.CharacterAbilities;
using LotR.Core.Effects.Modifiers;

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
    }
}
