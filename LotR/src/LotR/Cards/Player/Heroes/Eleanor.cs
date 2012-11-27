using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Encounter.Treacheries;
using LotR.Effects;

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

            public override IEffectHandle GetHandle(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (exhaustable == null)
                    return base.GetHandle(game);

                var cost = new ExhaustSelf(exhaustable);
                return new EffectHandle(this, cost);
            }

            public void DuringEncounterCardRevealed(IGame game)
            {
                if (game.StagingArea.RevealedEncounterCard == null)
                    return;

                if ((!(game.StagingArea.RevealedEncounterCard is IRevealableCard)) || (!(game.StagingArea.RevealedEncounterCard is ITreacheryCard)))
                    return;

                game.AddEffect(this);
            }

            public override void Validate(IGame game, IEffectHandle handle)
            {
                var exhaustPayment = handle.Payment as IExhaustCardPayment;
                if (exhaustPayment == null)
                {
                    handle.Reject();
                    return;
                }

                if (exhaustPayment.Exhaustable.IsExhausted)
                {
                    handle.Reject();
                    return;
                }

                exhaustPayment.Exhaustable.Exhaust();

                handle.Accept();
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                if (game.StagingArea.RevealedEncounterCard == null)
                {
                    handle.Cancel(string.Format("There is no revealed encounter card for '{0}' to cancel", CardSource.Title));
                    return;
                }

                if (!(game.StagingArea.RevealedEncounterCard.Card is ITreacheryCard) || !game.StagingArea.RevealedEncounterCard.Card.HasEffect<IWhenRevealedEffect>())
                {
                    handle.Cancel(string.Format("The revealed encounter card, '{0}', is not an Treachery with a 'When Revealed' effect for '{1}' to cancel", game.StagingArea.RevealedEncounterCard.Card.Title, CardSource.Title));
                    return;
                }

                var revealedTitle = game.StagingArea.RevealedEncounterCard.Title;

                game.StagingArea.CancelRevealedCard(this);
                game.StagingArea.RevealEncounterCards(1);

                handle.Resolve(string.Format("'{0}' cancelled to the 'When Revealed' effect of '{1}'", CardSource.Title, revealedTitle));
            }
        }
    }
}
