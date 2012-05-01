using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public abstract class CardBase
        : ICard
    {
        protected CardBase(string title, CardType type, CardSet set)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            
            this.title = title;
            this.type = type;
            this.set = set;
        }

        private readonly string title;
        private readonly CardType type;
        private readonly CardSet set;
        private readonly IList<IDeckRestriction> deckRestrictions = new List<IDeckRestriction>();
        private readonly IList<IPlayRestriction> playRestrictions = new List<IPlayRestriction>();
        private readonly IList<ITargetRestriction> targetRestrictions = new List<ITargetRestriction>();
        private readonly IList<ICost> playCosts = new List<ICost>();

        protected void AddDeckRescriction(IDeckRestriction restriction)
        {
            if (restriction == null)
                throw new ArgumentNullException("restriction");

            deckRestrictions.Add(restriction);
        }

        protected void AddPlayRestriction(IPlayRestriction restriction)
        {
            if (restriction == null)
                throw new ArgumentNullException("restriction");

            playRestrictions.Add(restriction);
        }

        protected void AddTargetRestriction(ITargetRestriction restriction)
        {
            if (restriction == null)
                throw new ArgumentNullException("restriction");

            targetRestrictions.Add(restriction);
        }

        protected void AddPlayCost(ICost cost)
        {
            if (cost == null)
                throw new ArgumentNullException("cost");

            playCosts.Add(cost);
        }

        public string Title
        {
            get { return title; }
        }

        public CardType Type
        {
            get { return type; }
        }

        public CardSet Set
        {
            get { return set; }
        }

        public IEnumerable<IDeckRestriction> DeckRestrictions
        {
            get { return deckRestrictions; }
        }

        public IEnumerable<IPlayRestriction> PlayRestrictions
        {
            get { return playRestrictions; }
        }

        public IEnumerable<ITargetRestriction> TargetRestrictions
        {
            get { return targetRestrictions; }
        }

        public IEnumerable<ICost> PlayCosts
        {
            get { return playCosts; }
        }
    }
}
