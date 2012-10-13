using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChooseHero
        : IPlayerChoice
    {
        IHeroInPlay Hero { get; set; }
    }
}
