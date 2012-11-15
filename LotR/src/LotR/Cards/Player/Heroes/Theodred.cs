using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
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

            public override IEffectOptions GetOptions(IGame game)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return new EffectOptions();

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return new EffectOptions();

                var choice = new ChooseCharacter(CardSource, controller, questPhase.GetAllCharactersCommittedToQuest().OfType<ICharacterInPlay>().Where(x => x.Card is IHeroCard).ToList());
                return new EffectOptions(choice);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var chooseCharacter = options.Choice as IChooseCharacter;
                if (chooseCharacter == null || chooseCharacter.ChosenCharacter == null)
                    return GetCancelledString();

                chooseCharacter.ChosenCharacter.Resources += 1;

                return ToString();
            }
        }
    }
}
