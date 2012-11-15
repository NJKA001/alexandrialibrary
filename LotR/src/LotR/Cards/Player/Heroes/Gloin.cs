using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public class Gloin
        : HeroCardBase
    {
        public Gloin()
            : base("Gloin", CardSet.Core, 3, Sphere.Leadership, 9, 2, 2, 1, 4)
        {
            AddTrait(Trait.Dwarf);
            AddTrait(Trait.Noble);

            AddEffect(new ResponseAddResourcesForDamageTaken(this));
        }

        private class ResponseAddResourcesForDamageTaken
            : ResponseCharacterAbilityBase, IAfterDamageDealtToCharacter
        {
            public ResponseAddResourcesForDamageTaken(Gloin source)
                : base("After Gloin suffers damage, add 1 resource to his resource pool for each point of damage he just suffered.", source)
            {
            }

            public void AfterDamageDealtToCharacter(IDamageDealt state)
            {
                if (state.Damage == 0 || state.Target.Card.Id != Source.Id)
                    return;

                var character = state.Game.GetCardInPlay<ICharacterInPlay>(Source.Id);
                if (character == null)
                    return;

                state.AddEffect(this);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var damageDealt = game.CurrentPhase.GetDamageDealt().Where(x => x.Target.Card.Id == Source.Id).FirstOrDefault();
                if (damageDealt == null || damageDealt.Damage == 0)
                    return GetCancelledString();

                var resourceful = game.GetCardInPlay<ICharacterInPlay>(Source.Id);
                if (resourceful == null)
                    return GetCancelledString();

                resourceful.Resources += damageDealt.Damage;

                return ToString();
            }
        }
    }
}
