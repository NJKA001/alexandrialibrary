using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Refresh;
using LotR.States;
using LotR.States.Phases.Refresh;

namespace LotR.Cards.Encounter.Treacheries
{
    public class CaughtInAWeb
        : AttachableTreacheryCardBase
    {
        public CaughtInAWeb()
            : base("Caught in a Web", CardSet.Core, 80)
        {
            AddTrait(Trait.Condition);

            AddEffect(new WhenRevealedAttachToAHero(this));
            AddEffect(new HeroDoesNotReadyUnlessControllerPays(this));
        }

        private class WhenRevealedAttachToAHero
            : WhenRevealedEffect
        {
            public WhenRevealedAttachToAHero(CaughtInAWeb source)
                : base("The player with the highest threat level attached this card to one of his heroes. (Counts as a Condition attachment with the text: \"Attached hero does not ready during the refresh phase unless you pay 2 resources from that hero's pool.\")", source)
            {
            }

            public override IChoice GetChoice(IGameState state)
            {
                IPlayer mostThreateningPlayer = null;
                foreach (var player in state.GetStates<IPlayer>())
                {
                    if (mostThreateningPlayer == null || player.CurrentThreat > mostThreateningPlayer.CurrentThreat)
                    {
                        mostThreateningPlayer = player;
                    }
                }

                return new LotR.Effects.Choices.ChooseHero(Source, mostThreateningPlayer);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var heroChoice = choice as IChooseHero;
                if (heroChoice == null || heroChoice.Hero == null)
                    return;

                var attachmentHost = heroChoice.Hero as IAttachmentHostInPlay;
                if (attachmentHost == null)
                    return;

                var attachable = this.Source as IAttachableCard;
                if (attachable == null)
                    return;

                attachmentHost.AddAttachment(new AttachableInPlay(attachable, attachmentHost));
            }
        }

        private class HeroDoesNotReadyUnlessControllerPays
            : PassiveCardEffectBase, IDuringReadyingCard
        {
            public HeroDoesNotReadyUnlessControllerPays(CaughtInAWeb source)
                : base("Attached hero does not ready during the refresh phase unless you pay 2 resources from that hero's pool", source)
            {
            }

            public void DuringReadyingCard(ICardReadying state)
            {
                if (state.Exhaustable.Card.Id != Source.Id)
                    return;

                if (!state.Exhaustable.IsExhausted)
                    return;

                state.IsReadying = false;

                state.AddEffect(this);
            }

            public override ICost GetCost(IGameState state)
            {
                var attachment = state.GetState<IAttachableInPlay>(Source.Id);
                if (attachment == null || attachment.AttachedTo == null)
                    return null;

                var resourceful = attachment.AttachedTo as IResourcefulInPlay;
                if (resourceful == null)
                    return null;

                var cost = new PayResourcesFrom(Source, resourceful, 2, false);
                cost.IsOptional = true;
                return cost;
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var cardReadying = state.GetStates<ICardReadying>().Where(x => x.Exhaustable.Card.Id == Source.Id).FirstOrDefault();
                if (cardReadying == null)
                    return;

                var resourcePayment = payment as IResourcePayment;
                if (resourcePayment == null && resourcePayment.Payments.Count() != 1 || resourcePayment.Payments.First() == null || resourcePayment.Payments.First().Item1.Card.Id != Source.Id || resourcePayment.Payments.First().Item2 != 2)
                    return;

                cardReadying.IsReadying = true;
            }
        }
    }
}
