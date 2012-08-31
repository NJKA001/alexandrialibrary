using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IQuestCard
        : IProgressableCard, IRevealableCard
    {
        byte Sequence { get; }
        ICardEffect Setup { get; }
    }
}
