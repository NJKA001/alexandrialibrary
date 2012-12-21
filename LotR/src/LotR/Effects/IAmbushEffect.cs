using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases.Any;

namespace LotR.Effects
{
    public interface IAmbushEffect
        : ICardEffect, IAfterCardEntersPlay
    {
    }
}
