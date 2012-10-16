using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Allies;

namespace LotR.States
{
    public interface IAllyInPlay
        : IPlayerCardInPlay<IAllyCard>
    {
    }
}
