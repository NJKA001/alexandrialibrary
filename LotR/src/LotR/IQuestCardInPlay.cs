using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IQuestCardInPlay
        : IProgressableInPlay
    {
        new IQuestCard Card { get; }
    }
}
