using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Treasures;

namespace LotR.States
{
    public interface ITreasureInPlay
        : IPlayerCardInPlay<ITreasureCard>
    {
    }
}
