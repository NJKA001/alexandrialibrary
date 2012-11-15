using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Cards.Player.Heroes
{
    public class Denethor
        : HeroCardBase
    {
        public Denethor()
            : base("Denethor", CardSet.Core, 10, Sphere.Lore, 8, 1, 1, 3, 3)
        {
            AddTrait(Trait.Gondor);
            AddTrait(Trait.Noble);
            AddTrait(Trait.Steward);

            AddEffect(new ExamineTopCardOfEncounterDeck(this));
        }

        public class ExamineTopCardOfEncounterDeck
            : ActionCharacterAbilityBase
        {
            public ExamineTopCardOfEncounterDeck(Denethor source)
                : base("Exhaust Denethor to look at the top card of the encounter deck. You may move that card to the bottom of the deck.", source)
            {
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetOptions(game);

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                if (exhaustable == null)
                    return base.GetOptions(game);

                var cost = new ExhaustSelf(exhaustable);
                return new EffectOptions(null, cost);
            }

            public override bool PaymentAccepted(IGame game, IEffectOptions options)
            {
                var exhaustPayment = options.Payment as IExhaustCardPayment;
                if (exhaustPayment == null || exhaustPayment.Exhaustable == null || exhaustPayment.Exhaustable.IsExhausted)
                    return false;

                exhaustPayment.Exhaustable.Exhaust();

                var topCard = game.StagingArea.EncounterDeck.GetFromTop(1).FirstOrDefault();
                if (topCard == null)
                    return false;

                game.StagingArea.AddExaminedEncounterCards(new List<IEncounterCard> { topCard });

                return true;
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var topOfDeckChoice = options.Choice as IChooseTopOrBottomOfDeck;
                if (topOfDeckChoice == null)
                    return GetCancelledString();

                if (game.StagingArea.ExaminedEncounterCards.Count() != 1)
                    return GetCancelledString();

                var topCard = game.StagingArea.ExaminedEncounterCards.FirstOrDefault() as IEncounterCard;
                if (topCard == null)
                    return GetCancelledString();

                game.StagingArea.RemoveExaminedEncounterCards(new List<IEncounterCard> { topCard });

                if (topOfDeckChoice.TopOfDeck)
                {
                    game.StagingArea.EncounterDeck.PutOnTop(new List<IEncounterCard> { topCard });
                }
                else
                {
                    game.StagingArea.EncounterDeck.PutOnBottom(new List<IEncounterCard> { topCard });

                }

                return ToString();
            }
        }
    }
}
