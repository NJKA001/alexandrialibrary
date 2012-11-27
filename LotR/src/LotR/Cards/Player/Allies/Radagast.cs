using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Allies
{
    public class Radagast
        : AllyCardBase, IResourcefulCard
    {
        public Radagast()
            : base("Radagast", CardSet.SoM, 59, Sphere.Neutral, 5, 2, 1, 1, 3)
        {
            AddTrait(Trait.Istari);

            AddEffect(new CanPayResourcesForCreatures(this));
            AddEffect(new SpendResourcesToHealCreatures(this));
        }

        private class CanPayResourcesForCreatures
            : PassiveCardEffectBase, IDuringCheckForResourceMatch
        {
            public CanPayResourcesForCreatures(Radagast source)
                : base("Radagast collects 1 resource each resource phase. These resources can be used to pay for Creature cards played from your hand.", source)
            {
            }

            public void DuringCheckForResourceMatch(ICheckForResourceMatch state)
            {
                if (state.CostlyCard == null)
                {
                    state.IsResourceMatch = false;
                    return;
                }

                if (state.CostlyCard.PrintedTraits.Contains(Trait.Creature))
                {
                    state.IsResourceMatch = true;
                    return;
                }
            }
        }

        private class SpendResourcesToHealCreatures
            : ActionCharacterAbilityBase
        {
            public SpendResourcesToHealCreatures(Radagast source)
                : base("Spend X resources for Radagast's pool to heal X wounds on any 1 Creature.", source)
            {
            }

            private void HealCreatureInPlay(IGame game, IEffectHandle handle, ICharacterInPlay creature)
            {
                var payFrom = handle.Cost as IPayResourcesFrom;
                if (payFrom == null)
                {
                    handle.Cancel("Could not heal a Creature, resource payment is invalid");
                    return;
                }

                if (payFrom.NumberOfResources == 0)
                {
                    handle.Cancel("Could not heal a Creature, no resources were paid");
                    return;
                }

                creature.Damage -= payFrom.NumberOfResources;
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var resourceful = game.GetCardInPlay<ICharacterInPlay>(CardSource.Id);
                if (resourceful == null)
                    return base.GetHandle(game);

                var creatures = game.GetAllCardsInPlay<ICharacterInPlay>().Where(x => x.HasTrait(Trait.Creature)).ToList();
                if (creatures.Count == 0)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder("Choose a Creature in play", game, controller)
                        .Question("Which Creature do you want to heal?")
                            .Answers(creatures, item => string.Format("{0} ({1} damage, {2} hit points)", item.Title, item.Damage, item.Card.PrintedHitPoints), (source, handle, creature) => HealCreatureInPlay(source, handle, creature));

                var choice = builder.ToChoice();
                var cost = new PayResourcesFrom(CardSource, resourceful, 0, true);

                return new EffectHandle(this, choice, cost);
            }

            public override void Validate(IGame game, IEffectHandle handle)
            {
                var resourcePayment = handle.Payment as IResourcePayment;
                if (resourcePayment == null)
                {
                    handle.Reject();
                    return;
                }

                if (resourcePayment.Characters.Count() != 0)
                {
                    handle.Reject();
                    return;
                }

                var character = resourcePayment.Characters.First();

                if (character.Card.Id != CardSource.Id)
                {
                    handle.Reject();
                    return;
                }

                var numberOfResources = resourcePayment.GetPaymentBy(character.Card.Id);
                if (numberOfResources == 0)
                {
                    handle.Reject();
                    return;
                }

                if (character.Resources < numberOfResources)
                {
                    handle.Reject();
                    return;
                }

                character.Resources -= numberOfResources;

                handle.Accept();
            }
        }
    }
}
