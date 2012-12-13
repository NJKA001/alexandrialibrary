using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Effects.Phases.Any
{
    public interface IPlayCardFromHandEffect
        : IFrameworkEffect, ICostlyEffect
    {
    }
}
