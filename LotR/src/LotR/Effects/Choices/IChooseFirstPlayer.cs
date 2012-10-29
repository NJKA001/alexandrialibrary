using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChooseFirstPlayer
        : IChoice
    {
        IPlayer FirstPlayer { get; set; }

        void ChooseRandomFirstPlayer();
    }
}
