using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;

using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;

namespace LotR.Cards.Encounter.Locations
{
    public class MountainsOfMirkwood
        : LocationCardBase
    {
        public MountainsOfMirkwood()
            : base("Mountains of Mirkwood", CardSet.Core, 78, EncounterSet.Spiders_of_Mirkwood, 3, 2, 3, 0)
        {
        }

        private class TravelRevealOneCardFromEncounterDeck
            : TravelEffectBase
        {
            public TravelRevealOneCardFromEncounterDeck(MountainsOfMirkwood source)
                : base("Reveal the top card of the encounter deck and add it to the staging area to travel here.", source)
            {
            }

            public override void Validate(IGame game, IEffectHandle handle)
            {
                if (game.StagingArea.EncounterDeck.Size == 0)
                {
                    handle.Reject();
                    return;
                }

                game.StagingArea.RevealEncounterCard();

                handle.Accept();
            }
        }

        private class AfterExploredPlayersSearchTopFiveCardsOfTheirDeck
            : ResponseCardEffectBase, IAfterLocationExplored
        {
            public AfterExploredPlayersSearchTopFiveCardsOfTheirDeck(MountainsOfMirkwood source)
                : base("After Mountains of Mirkwood leaves play as an explored location, each player may search the top 5 cards of his deck for 1 card and add it to his hand. Shuffle the rest of the searched cards back into their owners' decks.", source)
            {
            }

            public void AfterLocationExplored(ILocationExplored state)
            {
                if (state.Location.Card.Id != source.Id || !state.IsExplored)
                    return;

                state.AddEffect(this);
            }

            private void AddChosenCardToHand(IGame game, IEffectHandle handle, IPlayer player, IPlayerCard card)
            {
                player.Hand.AddCards(new List<IPlayerCard> { card });
                player.Deck.RemoveFromDeck(card);
                handle.Resolve(string.Format("{0} added '{1}' to their hand", player.Name, card.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var availableCards = new Dictionary<Guid, IList<IPlayerCard>>();
                foreach (var player in game.Players)
                {
                    var topFive = player.Deck.GetFromTop(5).ToList();
                    availableCards.Add(player.StateId, topFive);
                }

                var builder =
                    new ChoiceBuilder("Each player may search the top 5 cards of his deck for 1 card of their choice", game, game.FirstPlayer, true)
                        .Question("Each play searches")
                            .Answer("Yes", 1);

                foreach (var player in game.Players)
                {
                    var cards = player.Deck.GetFromTop(5).ToList();
                    if (cards.Count == 0)
                        continue;

                    builder.Question(string.Format("{0}, which card would you like to add to your hand?", player.Name))
                        .Answers(cards, item => item.Title, (source, handle, card) => AddChosenCardToHand(game, handle, player, card));
                }

                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
