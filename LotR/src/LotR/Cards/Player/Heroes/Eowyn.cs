using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Modifiers;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public class Eowyn
        : HeroCardBase
    {
        public Eowyn()
            : base("Eowyn", CardSet.Core, 7, Sphere.Spirit, 9, 4, 1, 1, 3)
        {
            AddTrait(Trait.Noble);
            AddTrait(Trait.Rohan);

            AddEffect(new DiscardACardToAddOneWillpower(this));
        }

        public class DiscardACardToAddOneWillpower
            : ActionCharacterAbilityBase
        {
            public DiscardACardToAddOneWillpower(Eowyn source)
                : base("Discard 1 card from your hand to give Eowyn +1 Willpower until the end of the phase. This effect may be triggered by each player once per round.", source)
            {
            }

            private void PlayerDiscardsOneCard(IGame game, IEffectHandle handle, IPlayer player, IPlayerCard card)
            {
                if (player.Hand.Cards.Count() == 0)
                {
                    handle.Cancel(string.Format("{0} does not have any cards in their hand to discard", player.Name));
                    return;
                }

                player.DiscardFromHand(new List<IPlayerCard> { card });

                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                {
                    handle.Cancel(string.Format("Could not determine the controller of {0}", CardSource.Title));
                    return;
                }

                var willpowerful = controller.CardsInPlay.OfType<IWillpowerfulInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                if (willpowerful == null)
                {
                    handle.Cancel(string.Format("'{0}' is no longer in play", CardSource.Title));
                    return;
                }

                game.AddEffect(new WillpowerModifier(game.CurrentPhase.Code, source, willpowerful, TimeScope.Phase, 1));

                handle.Resolve(string.Format("{0} discarded a card to give '{0}' +1 Willpower until the end of the phase", player.Name, CardSource.Title));
            }

            private void CancelDiscard(IGame game, IEffectHandle handle, IPlayer player)
            {
                handle.Cancel(string.Format("{0} decided not to discard a card from their hand", player.Name));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var limit = new Limit(PlayerScope.AnyPlayer, TimeScope.Round, 1);

                var handSize = game.ActivePlayer.Hand.Cards.Count();

                if (handSize == 0)
                {
                    return base.GetHandle(game);
                }
                else
                {
                    var builder =
                        new ChoiceBuilder(string.Format("The active player may discard a card from their hand to give '{0}' +1 Willpower until the end of the phase", CardSource.Title), game, game.ActivePlayer);

                    if (handSize == 1)
                    {
                        builder.Question("You only have 1 card in your hand, do you want to discard it?")
                            .Answer("Yes, I will discard it", game.ActivePlayer.Hand.Cards.First(), (source, handle, card) => PlayerDiscardsOneCard(source, handle, game.ActivePlayer, card))
                            .LastAnswer("No, I will not discard my last card from my hand", false, (source, handle, item) => CancelDiscard(source, handle, game.ActivePlayer));
                    }
                    else
                    {
                        builder.Question(string.Format("{0}, do you want to discard a card from your hand?", game.ActivePlayer.Name))
                            .Answer("Yes, I will discard a card from my hand", true)
                                .Question("Which card will you discard?")
                                    .LastAnswers(game.ActivePlayer.Hand.Cards.ToList(), (item) => item.Title, (source, handle, card) => PlayerDiscardsOneCard(source, handle, game.ActivePlayer, card))
                            .LastAnswer("No, I will not discard a card from my hand", false, (source, handle, item) => CancelDiscard(source, handle, game.ActivePlayer));
                    }

                    return new EffectHandle(this, builder.ToChoice());
                }
            }
        }
    }
}
