using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Quest;

namespace LotR.Core.Heroes
{
    public class Theodred
        : HeroCardBase
    {
        public Theodred()
            : base("Theodred", Sphere.Leadership)
        {
            Trait(Traits.Noble);
            Trait(Traits.Rohan);
            Trait(Traits.Warrior);

            
        }

        #region Abilities

        public class AddResourceToCommittedHero
            : ResponseCharacterAbilityBase, IAfterCommitingToQuest
        {
            public AddResourceToCommittedHero(Theodred source)
                : base(source, "After Theodred commits to a quest, choose a hero committed to that quest. Add 1 resource to that hero's resource pool.")
            {
                theodred = source;
            }

            private Theodred theodred;

            public void Setup(ICommitToQuestStep step)
            {
                var self = step.CommitedCharacters.Where(x => x.Card == theodred).Select(x => x.Card).FirstOrDefault();

                if (self == null)
                    return;

                step.AddEffect(this);
            }

            public void Resolve(ICommitToQuestStep step)
            {

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
            //private ICardInPlay target;

            public ICard Source
            {
                get { return theodred; }
            }

            //public ICardInPlay Target
            //{
            //    get { return target; }
            //}

            public string Description
            {
                get { return "choose a hero committed to the quest"; }
            }

            public bool IsMetBy(IEnumerable<IPayment> payments)
            {
                if (payments == null)
                    return false;

                foreach (var payment in payments)
                {
                    var choosePayment = payment as IChooseCharacterPayment;
                    if (choosePayment != null)
                    {
                        var hero = choosePayment.Character as IHeroInPlay;
                        if (hero != null)
                        {
                            if (step.CommitedCharacters.Contains(hero))
                            {
                                //target = hero;
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
        }

        #endregion
    }
}
