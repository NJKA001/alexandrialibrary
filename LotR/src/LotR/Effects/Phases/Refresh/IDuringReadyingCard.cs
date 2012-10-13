using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Refresh;

namespace LotR.Effects.Phases.Refresh
{
    public interface IDuringReadyingCard
    {
        void DuringReadyingCard(ICardReadying state);
    }
}
