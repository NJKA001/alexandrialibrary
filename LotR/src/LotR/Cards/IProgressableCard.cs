using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards
{
    public interface IProgressableCard
        : IVictoryCard
    {
        byte QuestPoints { get; }
    }
}
