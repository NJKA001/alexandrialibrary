﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Quests;

namespace LotR.States
{
    public class QuestInPlay
        : CardInPlay<IQuestCard>, IQuestInPlay
    {
        public QuestInPlay(IGame game, IQuestCard card)
            : base(game, card)
        {
        }
    }
}
