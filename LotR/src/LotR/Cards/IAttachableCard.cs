using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;
using LotR.Effects.Phases;

namespace LotR.Cards
{
    public interface IAttachableCard
        : ICard
    {
        bool IsRestricted { get; }
        bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay);
    }
}
