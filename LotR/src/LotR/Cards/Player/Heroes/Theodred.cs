using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Quest;
using LotR.States;
using LotR.States.Phases.Quest;

namespace LotR.Cards.Player.Heroes
{
    public class Theodred
        : HeroCardBase
    {
        public Theodred()
            : base("Theodred", CardSet.Core, 2, Sphere.Leadership, 8, 1, 2, 1, 4)
        {
            AddTrait(Trait.Noble);
            AddTrait(Trait.Rohan);
            AddTrait(Trait.Warrior);

            AddEffect(new AddResourceToCommittedHero(this));
        }

        public class AddResourceToCommittedHero
            : ResponseCharacterAbilityBase, IAfterCommittingToQuest
        {
            public AddResourceToCommittedHero(Theodred source)
                : base("After Theodred commits to a quest, choose a hero committed to that quest. Add 1 resource to that hero's resource pool.", source)
            {
            }

            public void AfterCommittingToQuest(IGame game)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return;

                var committed = questPhase.GetAllCharactersCommittedToQuest().Where(x => x.Card.Id == CardSource.Id).FirstOrDefault();
                if (committed == null)
                    return;

                game.AddEffect(this);
            }

            private void AddOneResourceToQuestingHero(IGame game, IEffectHandle handle, IPlayer player, IHeroInPlay hero)
            {
                hero.Resources += 1;

                handle.Resolve(string.Format("{0} chose to add one resource to the resource pool of '{1}'", player.Name, hero.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return new EffectHandle(this);

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    throw new InvalidOperationException("Could not find Theodred in play");

                var questingHeroes = questPhase.GetAllCharactersCommittedToQuest().OfType<IHeroInPlay>().ToList();
                if (questingHeroes.Count == 0)
                    return new EffectHandle(this);

                var builder =
                    new ChoiceBuilder("Chose a questing hero to add 1 resource to", game, controller)
                        .Question(string.Format("{0}, which questing hero do you want to add 1 resource to?", controller.Name))
                            .LastAnswers(questingHeroes, item => item.Title, (source, handle, hero) => AddOneResourceToQuestingHero(game, handle, controller, hero));

                var choice = builder.ToChoice();

                return new EffectHandle(this, choice);
            }
        }
    }
}
