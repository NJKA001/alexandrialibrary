using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChoice
    {
        string Description { get; }
        bool IsOptional { get; set; }
        ISource Source { get; }
        IEnumerable<IPlayer> Players { get; }

        bool IsValid(IGame game);
    }
}
