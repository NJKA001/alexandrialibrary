using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public abstract class Mutable : Model, IMutable
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            IsChanged = false;
        }

        public bool IsChanged { get; protected set; }
    }
}
