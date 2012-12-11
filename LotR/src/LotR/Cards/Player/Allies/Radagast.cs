using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
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

            private void HealCreatureInPlay(IGame game, IEffectHandle handle, IPlayer controller, ICharacterInPlay radagast, ICharacterInPlay creature, byte numberOfResources)
            {
                radagast.Resources -= numberOfResources;
                creature.Damage -= numberOfResources;

                handle.Resolve(string.Format("{0} chose to have '{1}' heal {2} damage from '{3}'", controller.Name, CardSource.Title, numberOfResources, creature.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var resourceful = game.GetCardInPlay<ICharacterInPlay>(CardSource.Id);
                if (resourceful == null || resourceful.Resources == 0)
                    return base.GetHandle(game);

                var creatures = game.GetAllCardsInPlay<ICharacterInPlay>().Where(x => x.HasTrait(Trait.Creature) && x.Damage > 0).ToList();
                if (creatures.Count == 0)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder("Choose a Creature in play", game, controller);

                if (resourceful.Resources == 1)
                {
                    builder.Question(string.Format("'{0}' only has 1 resource available do you want to pay that one resource to heal a creature?", CardSource.Title))
                        .Answer(string.Format("Yes, I want to pay 1 resource from '{0}' to heal a creature", CardSource.Title), true)
                            .Question("Which Creature do you want to heal?")
                                .LastAnswers(creatures, item => string.Format("{0} ({1} damage, {2} hit points)", item.Title, item.Damage, item.Card.PrintedHitPoints), (source, handle, creature) => HealCreatureInPlay(source, handle, controller, resourceful, creature, 1));
                }
                else
                {
                    builder.Question("Which Creature do you want to heal?");

                    foreach (var creature in creatures)
                    {
                        //.Answers(creatures, item => string.Format("{0} ({1} damage, {2} hit points)", item.Title, item.Damage, item.Card.PrintedHitPoints), (source, handle, creature) => HealCreatureInPlay(source, handle, controller, resourceful, creature, 1));

                    //builder.Question(string.Format("How many resources do you want to pay from '{0}' to heal a creature?", CardSource.Title))
                        //.Answer(string.Format("Yes, I want to pay 1 resource from '{0}' to heal a creature", CardSource.Title), true)
                    }
                            
                }

                builder.LastAnswer(string.Format("No, I do not want to pay any resources from '{0}' to heal a creature", CardSource.Title), false, (source, handle, item) => handle.Cancel(string.Format("{0} chose not to have '{1}' pay any resources to heal a creature", controller.Name, CardSource.Title)));

                        

                
                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
