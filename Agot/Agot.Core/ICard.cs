using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface ICard
    {
        string Title { get; }
        CardType Type { get; }
        CardSet Set { get; }
        IEnumerable<IDeckRestriction> DeckRestrictions { get; }
        IEnumerable<IPlayRestriction> PlayRestrictions { get; }
        IEnumerable<ITargetRestriction> TargetRestrictions { get; }
        IEnumerable<ICost> PlayCosts { get; }
    }
}
