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

            public override IEffectHandle GetHandle(IGame game)
            {
                var limit = new Limit(PlayerScope.None, TimeScope.Round, 1);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return new EffectHandle(this, null, null, limit);

                var charactersToChooseFrom = game.GetAllCardsInPlay<ICharacterInPlay>().Where(x => x.Damage > 0).ToList();
                var choice = new ChooseCharacter(source, controller, charactersToChooseFrom);

                var resourceful = controller.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (resourceful == null)
                    return new EffectHandle(this, choice, null, limit);

                var cost = new PayResourcesFrom(source, resourceful, 1, false);

                return new EffectHandle(this, choice, cost, limit);
            }

            public override void Validate(IGame game, IEffectHandle handle)
            {
                var resourcePayment = handle.Payment as IResourcePayment;
                if (resourcePayment == null)
                {
                    handle.Reject();
                    return;
                }

                if (resourcePayment.Characters.Count() != 1)
                {
                    handle.Reject();
                    return;
                }

                var character = resourcePayment.Characters.First();

                if (character.Card.Id != source.Id)
                {
                    handle.Reject();
                    return;
                }

                if (resourcePayment.GetPaymentBy(character.Card.Id) != 1)
                {
                    handle.Reject();
                    return;
                }

                if (character.Resources < 1)
                {
                    handle.Reject();
                    return;
                }

                character.Resources -= 1;

                handle.Accept();
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var characterChoice = handle.Choice as IChooseCharacter;
                if (characterChoice == null || characterChoice.ChosenCharacter == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var damageable = characterChoice.ChosenCharacter as IDamagableInPlay;
                if (damageable == null || damageable.Damage == 0)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                damageable.Damage -= 1;

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
