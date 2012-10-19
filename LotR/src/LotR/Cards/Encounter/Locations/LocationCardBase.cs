using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;

namespace LotR.Cards.Encounter.Locations
{
    public abstract class LocationCardBase
        : ThreateningCardBase, ILocationCard
    {
        protected LocationCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte quantity, byte printedThreat, byte questPoints, byte victoryPoints)
            : base(CardType.Location, title, cardSet, cardNumber, encounterSet, quantity, printedThreat)
        {
            this.QuestPoints = questPoints;
            this.VictoryPoints = victoryPoints;
        }

        public virtual ITravelEffect Travel()
        {
            return Text.Effects.OfType<ITravelEffect>().FirstOrDefault();
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

        public virtual bool IsValidAttachment(IAttachableCard card)
        {
            return true;
        }
    }
}
