using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.Cards
{
    public interface ICardText
    {
        IEnumerable<ICardEffect> Effects { get; }
        string FlavorText { get; }
    }
}
