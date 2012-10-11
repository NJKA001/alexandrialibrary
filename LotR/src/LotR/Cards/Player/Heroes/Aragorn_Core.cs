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
    public sealed class Aragorn
        : HeroCardBase, IAfterCommittingToQuest
    {
        public Aragorn()
            : base("Aragorn", CardSet.Core, 1, Sphere.Leadership, 12, 2, 3, 2, 5)
        {
            AddTrait(Trait.Dunedain);
            AddTrait(Trait.Noble);
            AddTrait(Trait.Ranger);

            AddEffect(new SentinelAbility(this));
        }

        public void AfterCommittingToQuest(ICommitToQuestStep step)
        {
            var self = step.CommitedCharacters.Where(x => x.Card.Id == this.Id).Select(x => x.Card).FirstOrDefault();

            if (self == null)
                return;

            step.AddEffect(new ReadyAfterCommitingToQuest(this, step));
        }

        #region Abilities

        public class ReadyAfterCommitingToQuest
            : ResponseCharacterAbilityBase
        {
            public ReadyAfterCommitingToQuest(Aragorn source, ICommitToQuestStep step)
                : base("After Aragorn commits to a quest, spend 1 resource from his resource pool to ready him.", source)
            {
                aragorn = source;
                this.step = step;
            }

            private readonly Aragorn aragorn;
            private readonly ICommitToQuestStep step;

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                //var inPlay = step.GetCardInPlay(aragorn.Id) as ICardInPlay<IHeroCard>;

                //if (inPlay == null)
                //    return;

                //step.AddEffect(new ReadyCards(step, new List<ICardInPlay<IExhaustableCard>> { inPlay }));
            }

            public override ICost GetCost(IPhaseStep step)
            {
                return new ReadyCost(aragorn);
            }
        }

        public class ReadyCost
            : CostBase, ICost
        {
            public ReadyCost(Aragorn source)
                : base("Spend 1 resource from Aragorn's resource pool", source)
            {
            }

            public override bool IsMetBy(IPayment payment)
            {
                if (payment == null)
                    return false;

                var resourcePayment = payment as IResourcePayment;
                if (resourcePayment == null)
                    return false;

                if (resourcePayment.Payments.Count() != 1)
                    return false;

                if (resourcePayment.Payments.Sum(x => x.Item2) != 1)
                    return false;

                var payor = resourcePayment.Payments.Select(x => x.Item1).FirstOrDefault();
                if (payor == null)
                    return false;

                if (payor.Card.Id != Source.Id)
                    return false;

                return true;
            }
        }

        #endregion
    }
}
