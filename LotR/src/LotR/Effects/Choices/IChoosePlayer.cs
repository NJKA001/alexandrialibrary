using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChoosePlayer
        : IChoice
    {
        IPlayer ChosenPlayer { get; set; }
    }
}
