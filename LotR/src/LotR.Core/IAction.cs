using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IAction
        : ICardEffect
    {
        //void PayCost(IPlayer actingPlayer, ICardInPlay actingCard, IGame game);
    }
}
