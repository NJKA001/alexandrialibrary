using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects
{
    public interface IChoice
        : IChoiceItem
    {
        IEnumerable<IPlayer> Players { get; }
        IQuestion Question { get; }

        bool IsCancelled { get; set; }
        bool IsOptional { get; }

        bool IsValid(IGame game);
    }
}
