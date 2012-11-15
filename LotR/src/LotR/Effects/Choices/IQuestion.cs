using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Choices
{
    public interface IQuestion
        : IChoiceItem
    {
        IEnumerable<IAnswer> Answers { get; }

        uint MinimumChosenAnswers { get; }
        uint MaximumChosenAnswers { get; }
    }
}
