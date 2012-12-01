using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects;

namespace LotR.Effects.Costs
{
    public interface IPayResources
        : ICost
    {
        Sphere ResourceSphere { get; }
        byte NumberOfResources { get; }
        bool IsVariableCost { get; }
    }
}
