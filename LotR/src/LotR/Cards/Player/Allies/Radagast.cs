using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
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
                : base("Resources on Radagast can be used to pay for Creature cards played from your hand.", source)
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

            public override IEffectHandle GetHandle(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var charactersToChooseFrom = game.GetAllCardsInPlay<ICharacterInPlay>().Where(x => x.HasTrait(Trait.Creature)).ToList();
                var choice = new ChooseCharacterWithTrait(source, controller, Trait.Creature, charactersToChooseFrom);

                var resourceful = game.GetCardInPlay<ICharacterInPlay>(source.Id);
                if (resourceful == null)
                    return new EffectHandle(choice);

                var cost = new PayResourcesFrom(source, resourceful, 0, true);

                return new EffectHandle(choice, cost);
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

            public override void Resolve(IGame game, IEffectHandle handle)
            {
                var resourcePayment = handle.Payment as IResourcePayment;
                if (resourcePayment == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var numberOfResources = resourcePayment.GetPaymentBy(CardSource.Id);
                if (numberOfResources == 0)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var creatureChoice = handle.Choice as IChooseCharacterWithTrait;
                if (creatureChoice == null || creatureChoice.ChosenCharacter == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                if (!creatureChoice.ChosenCharacter.HasTrait(Trait.Creature))
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var damageable = creatureChoice.ChosenCharacter as IDamagableInPlay;
                if (damageable == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                damageable.Damage -= numberOfResources;

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
