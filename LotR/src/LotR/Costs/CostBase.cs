using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Costs
{
    public abstract class CostBase
        : ICost
    {
        protected CostBase(string description, ICard source)
        {
            this.Description = description;
            this.Source = source;
        }

        public ICard Source
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public abstract bool IsMetBy(IPayment payment);
    }
}
