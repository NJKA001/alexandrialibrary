using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Choices;
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
                game.StagingArea.RevealEncounterCards(1);

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

            public override IEffectHandle GetHandle(IGame game)
            {
                var availableCards = new Dictionary<Guid, IList<IPlayerCard>>();
                foreach (var player in game.Players)
                {
                    var topFive = player.Deck.GetFromTop(5).ToList();
                    availableCards.Add(player.StateId, topFive);
                }

                return new EffectHandle(this, new PlayersChooseCards<IPlayerCard>("each player may search the top 5 cards of his deck for 1 card of their choice", source, game.Players, 1, availableCards));
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var cardChoice = handle.Choice as IPlayersChooseCards<IPlayerCard>;
                if (cardChoice == null)
                {
                    handle.Cancel(GetCancelledString());
                    return;
                }

                foreach (var player in cardChoice.Players)
                {
                    var availableCards = cardChoice.GetAvailableCards(player.StateId);
                    if (availableCards.Count() == 0)
                        continue;

                    var chosenCard = cardChoice.GetChosenCards(player.StateId).FirstOrDefault() as IPlayerCard;
                    if (chosenCard != null)
                    {
                        var unchosenCards = availableCards.Where(x => x.Id != chosenCard.Id).OfType<IPlayerCard>();
                        player.Deck.ShuffleIn(unchosenCards);
                        player.Hand.AddCards(new List<IPlayerCard> { chosenCard });
                    }
                    else
                    {
                        player.Deck.ShuffleIn(availableCards.OfType<IPlayerCard>());
                    }
                }

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
