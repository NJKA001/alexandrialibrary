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

            private void HealOneDamageOnCharacter(IGame game, IEffectHandle handle, IPlayer player, ICharacterInPlay glorfindel, ICharacterInPlay character)
            {
                glorfindel.Resources -= 1;
                character.Damage -= 1;

                handle.Resolve(string.Format("{0} chose to heal 1 damage on '{1}'", player.Name, character.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var limit = new Limit(PlayerScope.None, TimeScope.Round, 1);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var resourceful = controller.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (resourceful == null || resourceful.Resources == 0)
                    return base.GetHandle(game);

                var characters = game.GetAllCardsInPlay<ICharacterInPlay>().Where(x => x.Damage > 0).ToList();
                if (characters.Count == 0)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder("Choose a wounded character in play to heal", game, controller)
                        .Question("Which character do you want to heal 1 damage on?")
                            .Answers(characters, item => string.Format("{0} ({1} damage of {2} hit points)", item.Title, item.Damage, item.Card.PrintedHitPoints), (source, handle, character) => HealOneDamageOnCharacter(game, handle, controller, resourceful, character))
                            .LastAnswer(string.Format("No, I do not want to pay 1 resource from '{0}' to heal a character", CardSource.Title), false, (source, handle, item) => handle.Cancel(string.Format("{0} chose not to pay 1 resource from '{1}' to heal a character", controller.Name, CardSource.Title)));
                
                return new EffectHandle(this, builder.ToChoice(), limit);
            }
        }
    }
}
