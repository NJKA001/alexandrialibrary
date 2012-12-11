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

            private void ExhaustToCancelRevealedTreachery(IGame game, IEffectHandle handle, IExhaustableInPlay exhaustable)
            {
                exhaustable.Exhaust();

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

                handle.Resolve(string.Format("The 'when revealed' effect of '{1}' has been cancelled", CardSource.Title, revealedTitle));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                if (game.StagingArea.RevealedEncounterCard == null)
                {
                    return base.GetHandle(game);
                }

                if (!(game.StagingArea.RevealedEncounterCard.Card is ITreacheryCard) || !game.StagingArea.RevealedEncounterCard.Card.HasEffect<IWhenRevealedEffect>())
                {
                    return base.GetHandle(game);
                }

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (exhaustable == null || exhaustable.IsExhausted)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder(string.Format("Exhaust '{0}' to cancel the when revealed effects of a treachery just revealed by the encounter deck", CardSource.Title), game, controller)
                        .Question(string.Format("{0}, do you want to exhaust '{1}' to cancel the revealed treachery?", controller.Name, CardSource.Title))
                            .Answer(string.Format("Yes, exhaust '{0}' to cancel the revealed treachery", CardSource.Title), exhaustable, (source, handle, item) => ExhaustToCancelRevealedTreachery(source, handle, exhaustable));

                return new EffectHandle(this, builder.ToChoice());
            }

            public void DuringEncounterCardRevealed(IGame game)
            {
                if (game.StagingArea.RevealedEncounterCard == null)
                    return;

                if ((!(game.StagingArea.RevealedEncounterCard is IRevealableCard)) || (!(game.StagingArea.RevealedEncounterCard is ITreacheryCard)))
                    return;

                game.AddEffect(this);
            }
        }
    }
}
