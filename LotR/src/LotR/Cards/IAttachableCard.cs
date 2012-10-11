using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Cards
{
    public interface IAttachableCard
        : ICard
    {
        bool IsRestricted { get; }
        bool CanBeAttachedTo(IGameState state, ICanHaveAttachments cardInPlay);
    }
}
