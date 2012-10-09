using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Games
{
    public interface ICharacterInPlay
        : ICardInPlay, IExhaustableInPlay, IDamageableInPlay
    {
        new ICharacterCard Card { get; }
    }
}
