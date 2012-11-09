using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects.Costs
{
    public interface IPayResources
        : ICost
    {
        Sphere Sphere { get; }
        byte NumberOfResources { get; }
        bool IsVariableCost { get; }
    }
}
