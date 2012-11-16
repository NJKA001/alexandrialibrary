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

            public override IEffectOptions GetOptions(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetOptions(game);

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (exhaustable == null)
                    return base.GetOptions(game);

                var cost = new ExhaustSelf(exhaustable);
                return new EffectOptions(cost);
            }

            public void DuringEncounterCardRevealed(IGame game)
            {
                if (game.StagingArea.RevealedEncounterCard == null)
                    return;

                if ((!(game.StagingArea.RevealedEncounterCard is IRevealableCard)) || (!(game.StagingArea.RevealedEncounterCard is ITreacheryCard)))
                    return;

                game.AddEffect(this);
            }

            public override bool PaymentAccepted(IGame game, IEffectOptions options)
            {
                var exhaustPayment = options.Payment as IExhaustCardPayment;
                if (exhaustPayment == null)
                    return false;

                if (exhaustPayment.Exhaustable.IsExhausted)
                    return false;

                exhaustPayment.Exhaustable.Exhaust();

                return true;
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                if (game.StagingArea.RevealedEncounterCard == null)
                    return GetCancelledString();

                game.StagingArea.CancelRevealedCard(this);
                game.StagingArea.EncounterDeck.Discard(new List<IEncounterCard> { game.StagingArea.RevealedEncounterCard });
                game.StagingArea.RevealEncounterCards(1);

                return ToString();
            }
        }
    }
}
