using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.States;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Quest;

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

        #region Abilities

        public class AddResourceToCommittedHero
            : ResponseCharacterAbilityBase, IAfterCommittingToQuest
        {
            public AddResourceToCommittedHero(Theodred source)
                : base("After Theodred commits to a quest, choose a hero committed to that quest. Add 1 resource to that hero's resource pool.", source)
            {
            }

            public void AfterCommittingToQuest(IGame game)
            {
                //var self = step.CommitedCharacters.Where(x => x.CardId == Source.Id).Select(x => x.Card).FirstOrDefault();

                //if (self == null)
                //    return;

                game.AddEffect(this);
            }

            public override ICost GetCost(IGame game)
            {
                //var commitStep = step as ICommitToQuestStep;
                //if (commitStep == null)
                //    return null;

                //return new ChooseHeroCommitedToTheQuest(Source, commitStep);
                return null;
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                //if (!(step is ICommitToQuestStep))
                //    return;

                //var heroChoice = choice as IChooseHero;
                //if (heroChoice == null || heroChoice.Hero == null)
                //    return;

                //step.AddEffect(new AddResources(step, new Dictionary<Guid, byte> { { heroChoice.Hero.CardId, 1 } }));
            }
        }

        #endregion
    }
}
