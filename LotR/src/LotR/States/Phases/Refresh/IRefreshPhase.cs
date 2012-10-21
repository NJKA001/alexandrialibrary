using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Refresh
{
    public interface IRefreshPhase
        : IPhase
    {
        IEnumerable<ICardReadying> GetCardsReadying();
    }
}
