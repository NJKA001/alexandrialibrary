using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Quests;

namespace LotR.States
{
    public interface IQuestInPlay
        : ICardInPlay<IQuestCard>
    {
    }
}
