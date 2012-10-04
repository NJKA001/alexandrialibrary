using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Costs;
using LotR.Core.Effects;
using LotR.Core.Effects.CharacterAbilities;
using LotR.Core.Payments;
using LotR.Core.Phases.Quest;

namespace LotR.Core.Heroes
{
    public class Theodred
        : HeroCardBase
    {
        public Theodred()
            : base("Theodred", SetNames.Core, 2, Sphere.Leadership, 8, 1, 2, 1, 4)
        {
            Trait(Traits.Noble);
            Trait(Traits.Rohan);
            Trait(Traits.Warrior);

            Effect(new AddResourceToCommittedHero(this));
        }

        #region Abilities

        public class AddResourceToCommittedHero
            : ResponseCharacterAbilityBase, IAfterCommittingToQuest
        {
            public AddResourceToCommittedHero(Theodred source)
                : base("After Theodred commits to a quest, choose a hero committed to that quest. Add 1 resource to that hero's resource pool.", source)
            {
            }

            public void AfterCommittingToQuest(ICommitToQuestStep step)
            {
                var self = step.CommitedCharacters.Where(x => x.CardId == Source.Id).Select(x => x.Card).FirstOrDefault();

                if (self == null)
                    return;

                step.AddEffect(this);
            }

            public override void Resolve(IPhaseStep step, IPayment payment)
            {
                if (payment == null)
                    return;

                if (!(step is ICommitToQuestStep))
                    return;

                var choice = payment as IChooseCharacterPayment;
                if (choice == null)
                    return;

                var hero = choice.Character as IHeroInPlay;
                if (hero == null)
                    return;

                step.AddEffect(new AddResources(step, new Dictionary<Guid, byte> { { hero.CardId, 1 } }));
            }

            public override ICost GetCost(IPhaseStep step)
            {
                var commitStep = step as ICommitToQuestStep;
                if (commitStep == null)
                    return null;

                return new ChooseHeroCommitedToTheQuest(Source, commitStep);
            }
        }

        #endregion
    }
}
