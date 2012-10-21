using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Cards.Player
{
    public interface IPlayerCard
        : ICard
    {
        IPlayer Owner { get; set; }
        Sphere PrintedSphere { get; }
    }
}
