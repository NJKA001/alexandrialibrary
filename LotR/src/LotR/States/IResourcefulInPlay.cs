using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects.Phases.Any;

namespace LotR.States
{
    public interface IResourcefulInPlay
        : ICardInPlay<IResourcefulCard>
    {
        byte Resources { get; set; }

        bool HasResourceIcon(ICostlyCard costlyCard, Sphere sphere);
    }
}
