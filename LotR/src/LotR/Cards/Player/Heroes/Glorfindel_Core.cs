using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.States;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public class Glorfindel_Core
        : HeroCardBase
    {
        public Glorfindel_Core()
            : base("Glorfindel", CardSet.Core, 11, Sphere.Lore, 12, 3, 3, 1, 5)
        {
            AddTrait(Trait.Noble);
            AddTrait(Trait.Noldor);
            AddTrait(Trait.Warrior);
        }

        private class PayOneResourceToHealCharacter
            : ActionCharacterAbilityBase
        {
            public PayOneResourceToHealCharacter(Glorfindel_Core source)
                : base("Pay 1 resource from Glordindel's pool to heal 1 damage on any character. (Limit once per round.)", source)
            {
            }

            public override IChoice GetChoice(IGame game)
            {
                return new ChooseCharacter(Source, game.ActivePlayer);
            }

            public override ICost GetCost(IGame game)
            {
                var resourceful = game.GetState<ICharacterInPlay>(Source.Id);
                if (resourceful == null)
                    return null;

                return new PayResourcesFrom(Source, resourceful, 1, false);
            }

            public override ILimit GetLimit(IGame game)
            {
                return new Limit(PlayerScope.None, TimeScope.Round, 1);
            }

            public override bool PaymentAccepted(IGame game, IPayment payment, IChoice choice)
            {
                if (payment == null)
                    return false;

                var resourcePayment = payment as IResourcePayment;
                if (resourcePayment == null)
                    return false;

                var firstPayment = resourcePayment.Payments.FirstOrDefault();
                if (firstPayment == null)
                    return false;

                if (firstPayment.Item1.Card.Id != Source.Id || firstPayment.Item2 != 1)
                    return false;

                if (firstPayment.Item1.Resources < 1)
                    return false;

                firstPayment.Item1.Resources -= 1;

                return true;
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var characterChoice = choice as IChooseCharacter;
                if (characterChoice == null || characterChoice.ChosenCharacter == null)
                    return;

                var damageable = characterChoice.ChosenCharacter as IDamagableInPlay;
                if (damageable == null || damageable.Damage == 0)
                    return;

                damageable.Damage -= 1;
            }
        }
    }
}
