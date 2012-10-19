using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;

namespace LotR.States
{
    public interface IHeroInPlay
        : IPlayerCardInPlay<IHeroCard>, IResourcefulInPlay
    {
    }
}
