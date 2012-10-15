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
    public sealed class Aragorn
        : HeroCardBase
    {
        public Aragorn()
            : base("Aragorn", CardSet.Core, 1, Sphere.Leadership, 12, 2, 3, 2, 5)
        {
            AddTrait(Trait.Dunedain);
            AddTrait(Trait.Noble);
            AddTrait(Trait.Ranger);

            AddEffect(new SentinelAbility(this));
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

            private readonly Aragorn aragorn;

            public void AfterCommittingToQuest(IGameState state)
            {
                var committedToQuest = state.GetStates<ICharactersCommittedToQuest>().FirstOrDefault();
                if (committedToQuest == null)
                    return;

                var self = committedToQuest.Characters.Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                if (self == null)
                    return;

                state.AddEffect(this);
            }

            public override bool PaymentAccepted(IGameState state, IPayment payment, IChoice choice)
            {
                var resourcePayment = payment as IResourcePayment;
                if (resourcePayment == null)
                    return false;

                if (resourcePayment.Payments.Count() != 1)
                    return false;

                var firstPayment = resourcePayment.Payments.FirstOrDefault();
                if (firstPayment == null)
                    return false;

                if (firstPayment.Item1.Card.Id != Source.Id || firstPayment.Item2 != 1)
                    return false;

                var resourceful = state.GetState<IResourcefulInPlay>(Source.Id);
                if (resourceful == null || resourceful.Resources < 1)
                    return false;

                resourceful.Resources -= firstPayment.Item2;

                return true;
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var exhaustable = state.GetState<IExhaustableInPlay>(Source.Id);
                if (exhaustable == null)
                    return;

                if (!exhaustable.IsExhausted)
                    return;

                exhaustable.Ready();
            }

            public override ICost GetCost(IGameState state)
            {
                return new ReadyCost(aragorn);
            }
        }

        public class ReadyCost
            : CostBase
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
