using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Effects.Choices
{
    public interface IChooseToKeepStartingHand
        : IChoice
    {
        IEnumerable<IPlayerCard> StartingHand { get; }

        bool KeepStartingHand { get; set; }
    }
}
