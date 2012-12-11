using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Quest;
using LotR.States;
using LotR.States.Phases.Quest;

namespace LotR.Cards.Player.Allies
{
    public class Bofur_Dw
        : AllyCardBase
    {
        public Bofur_Dw()
            : base("Bofur", CardSet.Dw, 6, Sphere.Spirit, 3, 2, 1, 1, 3)
        {
            this.IsUnique = true;

            AddTrait(Trait.Dwarf);
        }

        private class PayOneSpiritResourceToCommitToQuest
            : QuestActionCardEffectBase, IPlayerActionEffect, ICostlyEffect
        {
            public PayOneSpiritResourceToCommitToQuest(Bofur_Dw cardSource)
                : base("Spend 1 Spirit resource to put Bofur into play from your hand, exhausted and committed to a quest. If you quest successfully this phase and Bofur is still in play, return him to your hand.", cardSource)
            {
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                return base.GetHandle(game);
                //var cost = new PayResources(source, Sphere.Spirit, 1, false);
                //return new EffectHandle(this, //null, cost);
            }

            //public override void Validate(IGame game, IEffectHandle handle)
            //{
            //    var resourcePayment = handle.Payment as IResourcePayment;
            //    if (resourcePayment == null)
            //    {
            //        handle.Reject();
            //        return;
            //    }

            //    if (resourcePayment.Characters.Count() != 1)
            //    {
            //        handle.Reject();
            //        return;
            //    }

            //    var hero = resourcePayment.Characters.First() as IHeroInPlay;
            //    if (hero == null || !hero.HasResourceIcon(Sphere.Spirit))
            //    {
            //        handle.Reject();
            //        return;
            //    }

            //    if (resourcePayment.GetPaymentBy(hero.Card.Id) != 1)
            //    {
            //        handle.Reject();
            //        return;
            //    }

            //    hero.Resources -= 1;

            //    handle.Accept();
            //}

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var card = CardSource as IPlayerCard;
                if (card == null || card.Owner == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                var ally = card.Owner.Hand.Cards.Where(x => x.Id == source.Id).FirstOrDefault() as IAllyCard;
                if (ally == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                card.Owner.Hand.RemoveCards(new List<IPlayerCard> { ally });
                card.Owner.AddCardInPlay(new AllyInPlay(game, ally));

                game.AddEffect(new ReturnToHandAfterSuccessfulQuest(CardSource));

                handle.Resolve(GetCompletedStatus());
            }

            public Sphere ResourceSphere
            {
                get { return Sphere.Spirit; }
            }

            public byte NumberOfResources
            {
                get { return 1; }
            }

            public bool IsVariableCost
            {
                get { return false; }
            }
        }

        private class ReturnToHandAfterSuccessfulQuest
            : PassiveCardEffectBase, IAfterQuestResolution
        {
            public ReturnToHandAfterSuccessfulQuest(ICard cardSource)
                : base("If you quest successfully this phase and Bofur is still in play, return him to your hand.", cardSource)
            {
            }

            public void AfterQuestResolution(IQuestOutcome state)
            {
                if (!state.IsQuestSuccessful)
                    return;

                var cardInPlay = state.Game.GetCardInPlay<IAllyInPlay>(CardSource.Id);
                if (cardInPlay == null)
                    return;

                var controller = state.Game.GetController(CardSource.Id);
                if (controller == null)
                    return;

                controller.RemoveCardInPlay(cardInPlay);
                cardInPlay.Card.Owner.Hand.AddCards(new List<IPlayerCard> { cardInPlay.Card });
            }
        }
    }
}
