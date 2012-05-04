using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public abstract class PermanentBase
        : CardBase, IPermanent
    {
        protected PermanentBase(string title, CardType type, CardSet set, byte cost)
            : this(title, type, set, cost, cost)
        {
        }

        protected PermanentBase(string title, CardType type, CardSet set, byte cost, byte adjustedCost)
            : base(title, type, set)
        {
            this.cost = cost;
            this.adjustedCost = adjustedCost;
        }

        private readonly byte cost;
        private readonly byte adjustedCost;
        private readonly IList<HouseType> affiliations = new List<HouseType>();

        protected void AddAffiliation(HouseType affiliation)
        {
            affiliations.Add(affiliation);
        }

        public bool IsUnique
        {
            get;
            protected set;
        }

        public bool IsLimited
        {
            get;
            protected set;
        }

        public IEnumerable<HouseType> Affiliations
        {
            get { return affiliations; }
        }

        public byte Cost
        {
            get { return cost; }
        }

        public byte AdjustedCost
        {
            get { return adjustedCost; }
        }
    }
}
