using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface ITargetRestriction
    {
        bool IsFulfilledBy(ICardInPlay card);
    }
}
