using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;

namespace LotR.Games
{
    public interface IResourcefulInPlay
        : ICardInPlay
    {
        new IResourcefulCard Card { get; }

        bool HasResourceIcon(Sphere sphere);
    }
}
