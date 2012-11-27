using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;

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

            private void HealOneDamageOnCharacter(IGame game, IEffectHandle handle, IPlayer player, ICharacterInPlay character)
            {
                character.Damage -= 1;

                handle.Resolve(string.Format("{0} chose to heal 1 damage on '{1}'", player.Name, character.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var limit = new Limit(PlayerScope.None, TimeScope.Round, 1);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    throw new InvalidOperationException("Could not determine controller of Glorfindel");

                var characters = game.GetAllCardsInPlay<ICharacterInPlay>().Where(x => x.Damage > 0).ToList();
                if (characters.Count == 0)
                    return new EffectHandle(this);

                var builder =
                    new ChoiceBuilder("Choose a wounded character in play to heal", game, controller)
                        .Question("Which character do you want to heal 1 damage on?")
                            .LastAnswers(characters, item => string.Format("{0} ({1} damage of {2} hit points)", item.Title, item.Damage, item.Card.PrintedHitPoints), (source, handle, character)  => HealOneDamageOnCharacter(game, handle, controller, character));
                
                var choice = builder.ToChoice();

                var resourceful = controller.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (resourceful == null)
                    throw new InvalidOperationException("Could not find Glorfindel in play");

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
        }
    }
}
