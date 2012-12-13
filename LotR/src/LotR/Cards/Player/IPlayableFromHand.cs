using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases.Any;
using LotR.States;

namespace LotR.Cards.Player
{
    public interface IPlayableFromHand
        : IPlayerCard
    {
        IPlayCardFromHandEffect GetPlayFromHandEffect(IGame game, IPlayer player);
    }
}
