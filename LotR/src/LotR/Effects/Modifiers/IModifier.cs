using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Games.Phases;

namespace LotR.Effects.Modifiers
{
    public interface IModifier
        : IEffect
    {
        IPhase StartPhase { get; }
        ISource Source { get; }
        ICard Target { get; }
        TimeScope Duration { get; }
        int Value { get; }
    }
}
