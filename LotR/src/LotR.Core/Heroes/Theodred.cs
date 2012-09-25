using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects;
using LotR.Core.Effects.CharacterAbilities;
using LotR.Core.Phases.Quest;

namespace LotR.Core.Heroes
{
    public class Theodred
        : HeroCardBase, IAfterCommittingToQuest
    {
        public Theodred()
            : base("Theodred", SetNames.Core, 2, Sphere.Leadership, 8, 1, 2, 1, 4)
        {
            Trait(Traits.Noble);
            Trait(Traits.Rohan);
            Trait(Traits.Warrior);
        }

        public void AfterCommittingToQuest(ICommitToQuestStep step)
        {
            var self = step.CommitedCharacters.Where(x => x.CardId == this.Id).Select(x => x.Card).FirstOrDefault();

            if (self == null)
                return;

            step.AddEffect(new AddResourceToCommittedHero(this, step));
        }

        #region Abilities

        public class AddResourceToCommittedHero
            : ResponseCharacterAbilityBase
        {
            public AddResourceToCommittedHero(Theodred source, IPhaseStep step)
                : base("After Theodred commits to a quest, choose a hero committed to that quest. Add 1 resource to that hero's resource pool.", source)
            {
                theodred = source;
                this.step = step;
            }

            private readonly Theodred theodred;
            private readonly IPhaseStep step;

            public override void Resolve(IPayment payment)
            {
                if (payment == null)
                    return;

                var choice = payment as IChooseCharacterPayment;
                if (choice == null)
                    return;

                var hero = choice.Character as IHeroInPlay;
                if (hero == null)
                    return;

                step.AddEffect(new AddResources(step, new Dictionary<Guid, byte> { { hero.CardId, 1 } }));
            }
        }

        public class ChooseHeroCost
            : ICost
        {
            public ChooseHeroCost(Theodred source, ICommitToQuestStep step)
            {
                this.theodred = source;
                this.step = step;
            }

            private Theodred theodred;
            private ICommitToQuestStep step;

            public ICard Source
            {
                get { return theodred; }
            }

            public string Description
            {
                get { return "choose a hero committed to the quest"; }
            }

            public bool IsMetBy(IPayment payment)
            {
                if (payment == null)
                    return false;

                var choice = payment as IChooseCharacterPayment;
                if (choice == null)
                    return false;

                var hero = choice.Character as IHeroInPlay;
                if (hero == null)
                    return false;

                if (!step.CommitedCharacters.Contains(hero))
                    return false;

                return true;
            }
        }

        #endregion
    }
}
