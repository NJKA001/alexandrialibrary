using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;
using LotR.Effects;
using LotR.Effects.Costs;
using LotR.Effects.Phases.Any;
using LotR.States.Phases.Any;

namespace LotR.States
{
    public class HeroInPlay
        : CharacterInPlay<IHeroCard>, IHeroInPlay
    {
        public HeroInPlay(IGame game, IHeroCard card)
            : base(game, card)
        {
        }

        IHeroCard ICardInPlay<IHeroCard>.Card
        {
            get { return Card as IHeroCard; }
        }

        public override bool CanPayFor(ICostlyCard costlyCard)
        {
            if (costlyCard == null)
                throw new ArgumentNullException("costlyCard");

            if (base.CanPayFor(costlyCard))
                return true;

            var cost = costlyCard.GetResourceCost(Game) as IPayResources;
            if (cost == null || cost.Sphere == Sphere.Neutral)
                return true;

            return HasResourceIcon(cost.Sphere);
        }

        public override bool CanPayFor(ICardEffect cardEffect)
        {
            if (cardEffect == null)
                throw new ArgumentNullException("cardEffect");

            if (base.CanPayFor(cardEffect))
                return true;

            var handle = cardEffect.GetHandle(Game);
            var cost = handle.Cost;
            if (cost == null)
            {
                return true;
            }
            else if (cost is IPayResources)
            {
                var payResources = cost as IPayResources;
                if (payResources.Sphere == Sphere.Neutral)
                    return true;

                return HasResourceIcon(payResources.Sphere);
            }
            else if (cost is IPayResourcesFrom)
            {
                var payResourcesFrom = cost as IPayResourcesFrom;
                return (payResourcesFrom.Target.Card.Id == Card.Id);
            }
            
            return true;
        }

        public bool HasResourceIcon(Sphere sphere)
        {
            if (Card.PrintedSphere == sphere)
                return true;

            var check = new CheckForResourceIcon(Game, this, sphere);

            foreach (var attachment in Attachments.Where(x => x.HasEffect<IDuringCheckForResourceIcon>()))
            {
                foreach (var effect in attachment.Card.Text.Effects.OfType<IDuringCheckForResourceIcon>())
                {
                    effect.DuringCheckForResourceIcon(check);
                }
            }

            return check.HasResourceIcon;
        }

        public override bool HasTrait(Trait trait)
        {
            if (base.HasTrait(trait))
                return true;

            var check = new CheckForTrait(Game, this, trait);

            foreach (var attachment in Attachments.Where(x => x.Card.HasEffect<IDuringCheckForTrait>()))
            {
                foreach (var effect in attachment.Card.Text.Effects.OfType<IDuringCheckForTrait>())
                {
                    effect.DuringCheckForTrait(check);
                }
            }

            return check.HasTrait;
        }
    }
}
