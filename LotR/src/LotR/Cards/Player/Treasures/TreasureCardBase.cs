using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Costs;
using LotR.States;

namespace LotR.Cards.Player.Treasures
{
    public abstract class TreasureCardBase
        : CostlyCardBase, ITreasureCard
    {
        protected TreasureCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte printedCost)
            : base(CardType.Treasure, title, cardSet, cardNumber, Sphere.Neutral, printedCost)
        {
        }

        public EncounterSet EncounterSet
        {
            get;
            private set;
        }

        public bool IsRestricted
        {
            get;
            protected set;
        }

        public virtual bool CanBeAttachedTo(IGame game, ICanHaveAttachments attachmentHost)
        {
            if (!attachmentHost.IsValidAttachment(this))
                return false;

            var cardInPlay = attachmentHost as ICardInPlay;
            if (cardInPlay == null)
                return false;

            if ((!(cardInPlay is IHeroInPlay)) && cardInPlay.Title != "Gandalf")
                return false;

            return true;
        }
    }
}
