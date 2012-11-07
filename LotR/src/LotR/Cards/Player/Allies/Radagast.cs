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
                if (state.CostlyCard.PrintedTraits.Contains(Trait.Creature))
                    state.IsResourceMatch = true;
            }
        }

        private class SpendResourcesToHealCreatures
            : ActionCharacterAbilityBase
        {
            public SpendResourcesToHealCreatures(Radagast source)
                : base("Spend X resources for Radagast's pool to heal X wounds on any 1 Creature.", source)
            {
            }

            public override IChoice GetChoice(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return null;

                return new ChooseCharacterWithTrait(Source, controller, Trait.Creature);
            }

            public override ICost GetCost(IGame game)
            {
                var resourceful = game.GetCardInPlay<ICharacterInPlay>(Source.Id);
                if (resourceful == null)
                    return null;

                return new PayResourcesFrom(Source, resourceful, 0, true);
            }

            public override bool PaymentAccepted(IGame game, IPayment payment, IChoice choice)
            {
                var resourcePayment = payment as IResourcePayment;
                if (resourcePayment == null)
                    return false;

                if (resourcePayment.Payments.Count() != 0)
                    return false;

                var firstPayment = resourcePayment.Payments.FirstOrDefault();
                if (firstPayment == null)
                    return false;

                if (firstPayment.Item1.Card.Id != Source.Id)
                    return false;

                if (firstPayment.Item2 == 0)
                    return false;

                firstPayment.Item1.Resources -= firstPayment.Item2;

                return true;
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var resourcePayment = payment as IResourcePayment;
                if (resourcePayment == null)
                    return;

                var firstPayment = resourcePayment.Payments.FirstOrDefault();
                if (firstPayment == null || firstPayment.Item2 == 0)
                    return;

                var creatureChoice = choice as IChooseCharacterWithTrait;
                if (creatureChoice == null || creatureChoice.ChosenCharacter == null)
                    return;

                if (!creatureChoice.ChosenCharacter.HasTrait(Trait.Creature))
                    return;

                var damageable = creatureChoice.ChosenCharacter as IDamagableInPlay;
                if (damageable == null)
                    return;

                damageable.Damage -= firstPayment.Item2;
            }
        }
    }
}
