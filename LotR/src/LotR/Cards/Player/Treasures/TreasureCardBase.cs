using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Costs;
using LotR.States;

namespace LotR.Cards.Player.Treasures
{
    public abstract class TreasureCardBase
        : PlayerCardBase, ITreasureCard
    {
        protected TreasureCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte printedCost)
            : base(title, cardSet, cardNumber, Sphere.Neutral)
        {
            this.PrintedCost = printedCost;
        }

        public EncounterSet EncounterSet
        {
            get;
            private set;
        }

        public byte PrintedCost
        {
            get;
            private set;
        }

        public ICost GetResourceCost(IGameState state)
        {
            return new PayResources(this, PrintedSphere, PrintedCost, false);
        }

        public bool IsRestricted
        {
            get;
            protected set;
        }

        public virtual bool CanBeAttachedTo(IGameState state, ICanHaveAttachments attachmentHost)
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
