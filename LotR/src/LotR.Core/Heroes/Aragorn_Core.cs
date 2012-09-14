using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects;
using LotR.Core.Effects.CharacterAbilities;
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
            : ResponseCharacterAbilityBase, IAfterCommittingToQuest
        {
            public ReadyAfterCommitingToQuest(Aragorn source)
                : base("After Aragorn commits to a quest, spend 1 resource from his resource pool to ready him.", source)
            {
                aragorn = source;
            }

            private Aragorn aragorn;

            public void AfterCommittingToQuestSetup(ICommitToQuestStep step)
            {
                var self = step.CommitedCharacters.Where(x => x.Card == aragorn).Select(x => x.Card).FirstOrDefault();

                if (self == null)
                    return;

                step.AddEffect(this);
            }

            public void AfterCommittingToQuestResolve(ICommitToQuestStep step, IPayment payment)
            {
                var inPlay = step.GetCardInPlay(aragorn.Id) as IHeroInPlay;

                if (inPlay == null)
                    return;

                step.AddEffect(new ReadyCards(step, new List<IExhaustableCard> { inPlay }));
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

            public bool IsMetBy(IPayment payment)
            {
                if (payment == null)
                    return false;

                var resourcePayment = payment as IResourcePayment;
                if (resourcePayment != null)
                {
                    if (resourcePayment.Resources == 1 && resourcePayment.Source == aragorn)
                        return true;
                }

                return false;
            }
        }

        #endregion
    }
}
