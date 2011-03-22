using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public abstract class Deletable : Model, IDeletable
    {
        protected virtual void OnDelete()
        {
        }

        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            if (IsDeleted)
                throw new InvalidOperationException("This model has already been marked to be deleted. A model cannot be deleted twice.");

            OnDelete();

            IsDeleted = true;
        }
    }
}
