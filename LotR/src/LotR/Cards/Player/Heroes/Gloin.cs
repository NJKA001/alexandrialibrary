using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Combat;

namespace LotR.Cards.Player.Heroes
{
    public class Gloin
        : HeroCardBase, IAfterDamageDealtToCharacter
    {
        public Gloin()
            : base("Gloin", CardSet.Core, 3, Sphere.Leadership, 9, 2, 2, 1, 4)
        {
            AddTrait(Trait.Dwarf);
            AddTrait(Trait.Noble);
        }

        #region Abilities

        public class AddResourcesWhenTakingDamage
            : ResponseCharacterAbilityBase
        {
            public AddResourcesWhenTakingDamage(Gloin source, ICardInPlay<ICard> permanent, byte value)
                : base("After Gloin suffers damage, add 1 resource to his resource pool for each point of damage he just suffered.", source)
            {
                this.permanent = permanent;
                this.value = value;
            }

            private readonly ICardInPlay<ICard> permanent;
            private readonly byte value;

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                //permanent.AddResources(value);
            }
        }

        #endregion

        public void AfterDamageDealtToCharacter(IDealDamageToCharacterStep step)
        {
            //if (step.Target.CardId != this.Id)
            //    return;

            //step.AddEffect(new AddResourcesWhenTakingDamage(this, step.Target, step.Damage));
        }
    }
}
