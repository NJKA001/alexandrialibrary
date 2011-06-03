using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public abstract class ValueBase
        : IValue
    {
        protected ValueBase(Guid id)
        {
            this.id = id;
        }

        private readonly Guid id;

        public Guid Id
        {
            get { return id; }
        }
    }
}
