using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Treacheries;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Games;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public class Eleanor
        : HeroCardBase
    {
        public Eleanor()
            : base("Eleanor", CardSet.Core, 8, Sphere.Spirit, 7, 1, 1, 2, 3)
        {
            AddTrait(Trait.Gondor);
            AddTrait(Trait.Noble);

            AddEffect(new CancelWhenRevealedTreachery(this));
        }

        public class CancelWhenRevealedTreachery
            : ResponseCharacterAbilityBase, ICancelEffect, IAfterEncounterCardRevealed
        {
            public CancelWhenRevealedTreachery(Eleanor source)
                : base("Exhaust Eleanor to cancel the 'when revealed' effects of a treachery card just revealed by the encounter deck. Then, discard that card, and replace it with the next card from the encounter deck.", source)
            {
            }

            public override ICost GetCost(IPhaseStep step)
            {
                var exhaustable = step.GetCardInPlay(Source.Id) as IExhaustableInPlay;
                if (exhaustable == null)
                    return null;

                return new ExhaustSelf(exhaustable);
            }

            public void AfterEncounterCardRevealed(IEncounterCardRevealedStep step)
            {
                if (step.Card == null)
                    return;

                var revealed = step.Phase.Round.Game.StagingArea.RevealedEncounterCard;
                if (revealed == null)
                    return;

                if ((!(revealed is IRevealableCard)) || (!(revealed is ITreacheryCard)))
                    return;

                step.AddEffect(this);
            }

            public override bool PaymentAccepted(IPhaseStep step, IPayment payment)
            {
                if (payment == null)
                    return false;

                var exhaustPayment = payment as IExhaustCardPayment;
                if (exhaustPayment == null)
                    return false;

                exhaustPayment.Exhaustable.Exhaust();

                return true;
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                var revealed = step.Phase.Round.Game.StagingArea.RevealedEncounterCard;
                if (revealed == null)
                    return;

                step.Phase.Round.Game.StagingArea.CancelRevealedCard(this);
                step.Phase.Round.Game.StagingArea.AddToEncounterDiscardPile(new List<IEncounterCard> { revealed });
                step.Phase.Round.Game.StagingArea.RevealEncounterCards(1);
            }
        }
    }
}
