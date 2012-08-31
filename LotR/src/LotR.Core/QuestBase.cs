using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class QuestBase
        : CardBase, IQuestCard
    {
        public byte Sequence
        {
            get;
            protected set;
        }

        public ICardEffect Setup
        {
            get;
            protected set;
        }

        public byte QuestPoints
        {
            get;
            protected set;
        }

        public byte VictoryPoints
        {
            get;
            protected set;
        }

        public ICardEffect WhenRevealed
        {
            get;
            protected set;
        }
    }
}
