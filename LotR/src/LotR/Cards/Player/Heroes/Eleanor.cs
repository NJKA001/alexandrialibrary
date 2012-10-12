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
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Areas;

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
            : ResponseCharacterAbilityBase, ICancelEffect, IDuringEncounterCardRevealed
        {
            public CancelWhenRevealedTreachery(Eleanor source)
                : base("Exhaust Eleanor to cancel the 'when revealed' effects of a treachery card just revealed by the encounter deck. Then, discard that card, and replace it with the next card from the encounter deck.", source)
            {
            }

            public override ICost GetCost(IGameState state)
            {
                var exhaustable = state.GetState<IExhaustableInPlay>(Source.Id);
                if (exhaustable == null)
                    return null;

                return new ExhaustSelf(exhaustable);
            }

            public void DuringEncounterCardRevealed(IGameState state)
            {
                var stagingArea = state.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return;

                if (stagingArea.RevealedEncounterCard == null)
                    return;

                if ((!(stagingArea.RevealedEncounterCard is IRevealableCard)) || (!(stagingArea.RevealedEncounterCard is ITreacheryCard)))
                    return;

                state.AddEffect(this);
            }

            public override bool PaymentAccepted(IGameState state, IPayment payment)
            {
                if (payment == null)
                    return false;

                var exhaustPayment = payment as IExhaustCardPayment;
                if (exhaustPayment == null)
                    return false;

                if (exhaustPayment.Exhaustable.IsExhausted)
                    return false;

                exhaustPayment.Exhaustable.Exhaust();

                return true;
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var stagingArea = state.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return;

                if (stagingArea.RevealedEncounterCard == null)
                    return;

                stagingArea.CancelRevealedCard(this);
                stagingArea.EncounterDeck.Discard(new List<IEncounterCard> { stagingArea.RevealedEncounterCard });
                stagingArea.RevealEncounterCards(1);
            }
        }
    }
}
