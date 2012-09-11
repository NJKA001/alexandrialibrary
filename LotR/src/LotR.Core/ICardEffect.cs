using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ICardEffect
    {
        ICard Source { get; }
        string Description { get; }

        ICost GetCost();
    }
}
