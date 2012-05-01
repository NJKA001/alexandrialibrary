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
            : base(title, type, set)
        {
            this.cost = cost;
        }

        private readonly byte cost;
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

        public IText Text
        {
            get;
            protected set;
        }
    }
}
