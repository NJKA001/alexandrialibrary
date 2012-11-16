using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IQuestion
        : IChoiceItem
    {
        IPlayer Player { get; }
        IEnumerable<IAnswer> Answers { get; }

        uint MinimumChosenAnswers { get; }
        uint MaximumChosenAnswers { get; }
    }
}
