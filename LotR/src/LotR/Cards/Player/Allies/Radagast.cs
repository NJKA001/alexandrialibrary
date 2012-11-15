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

            public override IEffectOptions GetOptions(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetOptions(game);

                var charactersToChooseFrom = game.GetAllCardsInPlay<ICharacterInPlay>().Where(x => x.HasTrait(Trait.Creature)).ToList();
                var choice = new ChooseCharacterWithTrait(Source, controller, Trait.Creature, charactersToChooseFrom);

                var resourceful = game.GetCardInPlay<ICharacterInPlay>(Source.Id);
                if (resourceful == null)
                    return new EffectOptions(choice);

                var cost = new PayResourcesFrom(Source, resourceful, 0, true);

                return new EffectOptions(choice, cost);
            }

            public override bool PaymentAccepted(IGame game, IEffectOptions options)
            {
                var resourcePayment = options.Payment as IResourcePayment;
                if (resourcePayment == null)
                    return false;

                if (resourcePayment.Characters.Count() != 0)
                    return false;

                var character = resourcePayment.Characters.First();

                if (character.Card.Id != CardSource.Id)
                    return false;

                var numberOfResources = resourcePayment.GetPaymentBy(character.Card.Id);
                if (numberOfResources == 0)
                    return true;

                if (character.Resources < numberOfResources)
                    return false;

                character.Resources -= numberOfResources;

                return true;
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var resourcePayment = options.Payment as IResourcePayment;
                if (resourcePayment == null)
                    return GetCancelledString();

                var numberOfResources = resourcePayment.GetPaymentBy(CardSource.Id);
                if (numberOfResources == 0)
                    return GetCancelledString();

                var creatureChoice = options.Choice as IChooseCharacterWithTrait;
                if (creatureChoice == null || creatureChoice.ChosenCharacter == null)
                    return GetCancelledString();

                if (!creatureChoice.ChosenCharacter.HasTrait(Trait.Creature))
                    return GetCancelledString();

                var damageable = creatureChoice.ChosenCharacter as IDamagableInPlay;
                if (damageable == null)
                    return GetCancelledString();

                damageable.Damage -= numberOfResources;

                return ToString();
            }
        }
    }
}
