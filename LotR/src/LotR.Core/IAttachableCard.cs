using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IAttachableCard
        : ICard
    {
        bool IsRestricted { get; }
        bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay);
    }
}
