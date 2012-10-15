using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class PlayersChooseCards
        : ChoiceBase, IPlayersChooseCards
    {
        public PlayersChooseCards(string description, ISource source, IEnumerable<IPlayer> players, byte numberOfCards, IDictionary<Guid, IList<ICard>> availableCards)
            : base(description, source, players)
        {
            if (availableCards == null)
                throw new ArgumentNullException("availableCharacters");

            this.NumberOfCards = numberOfCards;
            this.availableCards = availableCards;
        }

        private readonly IDictionary<Guid, IList<ICard>> availableCards;
        private readonly IDictionary<Guid, IList<ICard>> chosenCards = new Dictionary<Guid, IList<ICard>>();

        public byte NumberOfCards
        {
            get;
            private set;
        }

        public IEnumerable<ICard> GetAvailableCards(Guid playerId)
        {
            return availableCards.ContainsKey(playerId) ? availableCards[playerId] : Enumerable.Empty<ICard>();
        }

        public IEnumerable<ICard> GetChosenCards(Guid playerId)
        {
            return chosenCards.ContainsKey(playerId) ? chosenCards[playerId] : Enumerable.Empty<ICard>();
        }

        public void AddChosenCard(Guid playerId, ICard card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            if (!Players.Select(x => x.StateId).Contains(playerId))
                return;

            if (!chosenCards.ContainsKey(playerId))
            {
                chosenCards[playerId] = new List<ICard> { card };
            }
            else
            {
                if (chosenCards[playerId].Any(x => x.Id == card.Id))
                    return;

                chosenCards[playerId].Add(card);
            }
        }

        public override bool IsValid(IGameState state)
        {
            foreach (var player in Players)
            {
                var availableForPlayer = GetAvailableCards(player.StateId);
                var chosenByPlayer = GetChosenCards(player.StateId);
                if (availableForPlayer.Count() >= NumberOfCards && chosenByPlayer.Count() < NumberOfCards)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
