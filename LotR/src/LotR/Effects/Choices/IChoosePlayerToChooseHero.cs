using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChoosePlayerToChooseHero
        : IChoice, IChooseHero
    {
        IEnumerable<IPlayer> PlayersToChooseFrom { get; }
        IPlayer ChosenPlayer { get; set; }
    }
}
