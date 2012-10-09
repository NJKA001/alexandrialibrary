using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Quests;

namespace LotR.Games
{
    public interface IQuestCardInPlay
        : IProgressableInPlay
    {
        new IQuestCard Card { get; }
    }
}
