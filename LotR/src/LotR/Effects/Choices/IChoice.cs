using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects.Choices
{
    public interface IChoice
    {
        string Description { get; }
        ISource Source { get; }
    }
}
