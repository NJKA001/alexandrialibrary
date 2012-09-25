﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects;
using LotR.Core.Effects.CharacterAbilities;
using LotR.Core.Phases.Quest;

namespace LotR.Core.Heroes
{
    public sealed class Aragorn
        : HeroCardBase, IAfterCommittingToQuest
    {
        public Aragorn()
            : base("Aragorn", SetNames.Core, 1, Sphere.Leadership, 12, 2, 3, 2, 5)
        {
            Trait(Traits.Dunedain);
            Trait(Traits.Noble);
            Trait(Traits.Ranger);

            Effect(new SentinelAbility(this));
        }

        public void AfterCommittingToQuest(ICommitToQuestStep step)
        {
            var self = step.CommitedCharacters.Where(x => x.CardId == this.Id).Select(x => x.Card).FirstOrDefault();

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

            public override void Resolve(IPayment payment)
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