using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;

namespace LotR.Games
{
    public interface IHeroInPlay
        : ICharacterInPlay
    {
        new IHeroCard Card { get; }
    }
}
