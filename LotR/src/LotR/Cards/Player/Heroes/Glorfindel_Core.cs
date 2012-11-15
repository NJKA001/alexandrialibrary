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

            public override IEffectOptions GetOptions(IGame game)
            {
                var limit = new Limit(PlayerScope.None, TimeScope.Round, 1);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return new EffectOptions(null, null, limit);

                var charactersToChooseFrom = game.GetAllCardsInPlay<ICharacterInPlay>().Where(x => x.Damage > 0).ToList();
                var choice = new ChooseCharacter(Source, controller, charactersToChooseFrom);

                var resourceful = controller.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                if (resourceful == null)
                    return new EffectOptions(choice, null, limit);

                var cost = new PayResourcesFrom(Source, resourceful, 1, false);

                return new EffectOptions(choice, cost, limit);
            }

            public override bool PaymentAccepted(IGame game, IEffectOptions options)
            {
                var resourcePayment = options.Payment as IResourcePayment;
                if (resourcePayment == null)
                    return false;

                if (resourcePayment.Characters.Count() != 1)
                    return false;

                var character = resourcePayment.Characters.First();

                if (character.Card.Id != Source.Id)
                    return false;

                if (resourcePayment.GetPaymentBy(character.Card.Id) != 1)
                    return false;

                if (character.Resources < 1)
                    return false;

                character.Resources -= 1;

                return true;
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var characterChoice = options.Choice as IChooseCharacter;
                if (characterChoice == null || characterChoice.ChosenCharacter == null)
                    return GetCancelledString();

                var damageable = characterChoice.ChosenCharacter as IDamagableInPlay;
                if (damageable == null || damageable.Damage == 0)
                    return GetCancelledString();

                damageable.Damage -= 1;

                return ToString();
            }
        }
    }
}
