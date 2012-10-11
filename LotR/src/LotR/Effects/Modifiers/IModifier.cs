using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Effects.Modifiers
{
    public interface IModifier
        : IEffect
    {
        Phase StartPhase { get; }
        ISource Source { get; }
        ICard Target { get; }
        TimeScope Duration { get; }
        int Value { get; }
    }
}
