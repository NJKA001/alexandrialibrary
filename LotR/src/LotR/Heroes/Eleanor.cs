using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Costs;
using LotR.Effects;
using LotR.Effects.CharacterAbilities;
using LotR.Payments;
using LotR.Phases.Any;

namespace LotR.Heroes
{
    public class Eleanor
        : HeroCardBase
    {
        public Eleanor()
            : base("Eleanor", SetNames.Core, 8, Sphere.Spirit, 7, 1, 1, 2, 3)
        {
            Trait(Traits.Gondor);
            Trait(Traits.Noble);

            Effect(new CancelWhenRevealedTreachery(this));
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
                var exhaustable = step.GetCardInPlay(Source.Id) as IExhaustableCard;
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

            public override void Resolve(IPhaseStep step, IPayment payment)
            {
                if (payment == null)
                    return;

                var exhaustPayment = payment as IExhaustCardPayment;
                if (exhaustPayment == null)
                    return;

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
