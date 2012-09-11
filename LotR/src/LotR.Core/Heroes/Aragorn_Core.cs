using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Quest;

namespace LotR.Core.Heroes
{
    public sealed class Aragorn
        : HeroCardBase
    {
        public Aragorn()
            : base("Aragorn", Sphere.Leadership)
        {
            Trait(Traits.Dunedain);
            Trait(Traits.Noble);
            Trait(Traits.Ranger);

            Effect(new SentinelAbility(this));
        }

        #region Abilities

        public class ReadyAfterCommitingToQuest
            : ResponseCharacterAbilityBase, IAfterCommitingToQuest
        {
            public ReadyAfterCommitingToQuest(Aragorn source)
                : base(source, "After Aragorn commits to a quest, spend 1 resource from his resource pool to ready him.")
            {
                aragorn = source;
            }

            private Aragorn aragorn;

            public void Setup(ICommitToQuestStep step)
            {
                var self = step.CommitedCharacters.Where(x => x.Card == aragorn).Select(x => x.Card).FirstOrDefault();

                if (self == null)
                    return;

                step.AddEffect(this);
            }

            public void Resolve(ICommitToQuestStep step)
            {
                var inPlay = step.GetCardInPlay(aragorn) as IHeroInPlay;

                if (inPlay == null)
                    return;

                inPlay.Ready();
            }

            public override ICost GetCost()
            {
                return new ReadyCost(aragorn);
            }
        }

        public class ReadyCost
            : ICost
        {
            public ReadyCost(Aragorn source)
            {
                aragorn = source;
            }

            private Aragorn aragorn;

            public ICard Source
            {
                get { return aragorn; }
            }

            public string Description
            {
                get { return "1 resource from Aragorn's resource pool"; }
            }

            public bool IsMetBy(IEnumerable<IPayment> payments)
            {
                if (payments == null)
                    return false;

                foreach (var payment in payments)
                {
                    var resourcePayment = payment as IResourcePayment;
                    if (resourcePayment != null)
                    {
                        if (resourcePayment.Resources == 1 && resourcePayment.Source == aragorn)
                            return true;
                    }
                }

                return false;
            }
        }

        #endregion
    }
}
