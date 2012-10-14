using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IPlayersChooseCharacters
        : IChoice
    {
        byte NumberOfCharacters { get; }
        
        IEnumerable<ICharacterInPlay> GetAvailableCharacters(Guid playerId);
        IEnumerable<ICharacterInPlay> GetChosenCharacters(Guid playerId);
        void AddChosenCharacter(Guid playerId, ICharacterInPlay character);
    }
}
