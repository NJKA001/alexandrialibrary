using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.Cards.Encounter
{
    public abstract class EncounterCardBase
        : CardBase, IEncounterCard
    {
        protected EncounterCardBase(CardType printedCardType, string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte quantity)
            : base(printedCardType, title, cardSet, cardNumber)
        {
            this.EncounterSet = encounterSet;
            this.Quantity = quantity;
        }

        public EncounterSet EncounterSet
        {
            get;
            private set;
        }

        public byte Quantity
        {
            get;
            private set;
        }

        public bool IsGuarded
        {
            get;
            protected set;
        }

        public virtual IWhenRevealedEffect WhenRevealed()
        {
            return Text.Effects.OfType<IWhenRevealedEffect>().FirstOrDefault();
        }

        public virtual IShadowEffect Shadow()
        {
            return Text.Effects.OfType<IShadowEffect>().FirstOrDefault();
        }
    }
}
