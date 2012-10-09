using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Quests
{
    public abstract class QuestCardBase
        : CardBase, IQuestCard
    {
        protected QuestCardBase(string title, CardSet cardSet, uint cardNumber, byte sequence, byte questPoints, byte victoryPoints)
            : base(title, cardSet, cardNumber)
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
