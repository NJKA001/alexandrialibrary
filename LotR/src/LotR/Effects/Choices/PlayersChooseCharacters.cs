using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public class PlayersChooseCharacters
        : ChoiceBase, IPlayersChooseCharacters
    {
        public PlayersChooseCharacters(string description, ISource source, IEnumerable<IPlayer> players, byte numberOfCharacters, IDictionary<Guid, IList<ICharacterInPlay>> availableCharacters)
            : base(description, source, players)
        {
            if (availableCharacters == null)
                throw new ArgumentNullException("availableCharacters");

            this.NumberOfCharacters = numberOfCharacters;
            this.availableCharacters = availableCharacters;
        }

        private readonly IDictionary<Guid, IList<ICharacterInPlay>> availableCharacters;
        private readonly IDictionary<Guid, IList<ICharacterInPlay>> chosenCharacters = new Dictionary<Guid, IList<ICharacterInPlay>>();

        public byte NumberOfCharacters
        {
            get;
            private set;
        }

        public IEnumerable<ICharacterInPlay> GetAvailableCharacters(Guid playerId)
        {
            return availableCharacters.ContainsKey(playerId) ? availableCharacters[playerId] : Enumerable.Empty<ICharacterInPlay>();
        }

        public IEnumerable<ICharacterInPlay> GetChosenCharacters(Guid playerId)
        {
            return chosenCharacters.ContainsKey(playerId) ? chosenCharacters[playerId] : Enumerable.Empty<ICharacterInPlay>();
        }

        public void AddChosenCharacter(Guid playerId, ICharacterInPlay character)
        {
            if (character == null)
                throw new ArgumentNullException("character");
            
            if (!Players.Select(x => x.StateId).Contains(playerId))
                throw new ArgumentException("playerId is not valid");

            if (!chosenCharacters.ContainsKey(playerId))
            {
                chosenCharacters[playerId] = new List<ICharacterInPlay> { character };
            }
            else
            {
                if (chosenCharacters[playerId].Any(x => x.Card.Id == character.Card.Id))
                    return;

                chosenCharacters[playerId].Add(character);
            }
        }

        public override bool IsValid(IGame game)
        {
            foreach (var player in Players)
            {
                if (availableCharacters.Count() >= NumberOfCharacters && GetChosenCharacters(player.StateId).Count() < NumberOfCharacters)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
