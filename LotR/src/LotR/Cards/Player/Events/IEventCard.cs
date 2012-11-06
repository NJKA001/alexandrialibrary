using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Cards.Player.Events
{
    public interface IEventCard
        : IPlayerActionCard, ICostlyCard
    {
    }
}
