using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChoice
        : IChoiceItem
    {
        ISource Source { get; }
        IEnumerable<IPlayer> Players { get; }
        IEnumerable<IQuestion> Questions { get; }

        bool IsCancelled { get; set; }
        bool IsOptional { get; }

        bool IsValid(IGame game);
    }
}
