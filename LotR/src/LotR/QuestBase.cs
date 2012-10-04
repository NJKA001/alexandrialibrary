using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class QuestBase
        : CardBase, IQuestCard
    {
        protected QuestBase(string title, string setName, uint setNumber, byte sequence, byte questPoints, byte victoryPoints)
            : base(title, setName, setNumber)
        {
            this.Sequence = sequence;
            this.QuestPoints = questPoints;
            this.VictoryPoints = victoryPoints;
        }

        public byte Sequence
        {
            get;
            private set;
        }

        public byte QuestPoints
        {
            get;
            private set;
        }

        public byte VictoryPoints
        {
            get;
            private set;
        }
    }
}
