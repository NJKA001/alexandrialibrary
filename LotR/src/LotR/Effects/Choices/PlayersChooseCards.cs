using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class PlayersChooseCards<T>
        : ChoiceBase, IPlayersChooseCards<T>
        where T : ICard
    {
        public PlayersChooseCards(string description, ISource source, IEnumerable<IPlayer> players, byte numberOfCards, IDictionary<Guid, IList<T>> availableCards)
            : base(description, source, players)
        {
            if (availableCards == null)
                throw new ArgumentNullException("availableCharacters");

            this.NumberOfCards = numberOfCards;
            this.availableCards = availableCards;
        }

        private readonly IDictionary<Guid, IList<T>> availableCards;
        private readonly IDictionary<Guid, IList<T>> chosenCards = new Dictionary<Guid, IList<T>>();

        public byte NumberOfCards
        {
            get;
            private set;
        }

        public IEnumerable<T> GetAvailableCards(Guid playerId)
        {
            return availableCards.ContainsKey(playerId) ? availableCards[playerId] : Enumerable.Empty<T>();
        }

        public IEnumerable<T> GetChosenCards(Guid playerId)
        {
            return chosenCards.ContainsKey(playerId) ? chosenCards[playerId] : Enumerable.Empty<T>();
        }

        public void AddChosenCard(Guid playerId, T card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            if (!Players.Select(x => x.StateId).Contains(playerId))
                return;

            if (!chosenCards.ContainsKey(playerId))
            {
                chosenCards[playerId] = new List<T> { card };
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
