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

            private void PutEncounterCardBackOnTopOfDeck(IGame game, IEffectHandle handle, IPlayer player, IEncounterCard encounterCard)
            {
                handle.Resolve(string.Format("{0} chose to put '{1}' back on the top of the encounter deck", player.Name, encounterCard.Title));
            }

            private void PutEncounterCardOnBottomOfDeck(IGame game, IEffectHandle handle, IPlayer player, IEncounterCard encounterCard)
            {
                game.StagingArea.EncounterDeck.RemoveFromDeck(encounterCard);
                game.StagingArea.EncounterDeck.PutOnBottom(new List<IEncounterCard> { encounterCard });
                
                handle.Resolve(string.Format("{0} chose to put '{1}' on the bottom of the encounter deck", player.Name, encounterCard.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return base.GetHandle(game);

                var exhaustable = controller.CardsInPlay.OfType<IExhaustableInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (exhaustable == null)
                    return base.GetHandle(game);

                var player = exhaustable.Card.Owner;

                var encounterCard = game.StagingArea.EncounterDeck.GetFromTop(1).FirstOrDefault();
                if (encounterCard == null)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder("Look at the top of the encounter deck. You may move that card to the bottom of the deck", game, player)
                        .Question(string.Format("{0}, do you want to put '{1}' on the bottom of the encounter deck?", player.Name, encounterCard.Title))
                            .Answer(string.Format("Yes, put '{0}' on the bottom of the encounter deck", encounterCard.Title), encounterCard, (source, handle, card) => PutEncounterCardOnBottomOfDeck(game, handle, player, card))
                            .LastAnswer(string.Format("No, put '{0}' back on the top of the encounter deck", encounterCard.Title), encounterCard, (source, handle, card) => PutEncounterCardBackOnTopOfDeck(game, handle, player, card));

                var cost = new ExhaustSelf(exhaustable);

                return new EffectHandle(this, builder.ToChoice(), cost);
            }

            public override void Validate(IGame game, IEffectHandle handle)
            {
                var exhaustPayment = handle.Payment as IExhaustCardPayment;
                if (exhaustPayment == null || exhaustPayment.Exhaustable == null || exhaustPayment.Exhaustable.IsExhausted)
                {
                    handle.Reject();
                    return;
                }

                exhaustPayment.Exhaustable.Exhaust();

                handle.Accept();
            }
        }
    }
}
