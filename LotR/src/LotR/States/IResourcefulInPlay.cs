using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases.Any;
using LotR.Cards.Player;

namespace LotR.States
{
    public interface IResourcefulInPlay
        : ICardInPlay<IResourcefulCard>, IDuringCheckForResourceIcon
    {
        byte Resources { get; set; }
    }
}
