using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public struct CollectionItemInfo
    {
        public CollectionItemInfo(object item, CollectionItemState state)
        {
            this.item = item;
            this.state = state;
            this.sequence = null;
        }

        public CollectionItemInfo(object item, CollectionItemState state, object sequence)
        {
            this.item = item;
            this.state = state;
            this.sequence = sequence;
        }

        private readonly object item;
        private readonly CollectionItemState state;
        private readonly object sequence;

        public object Item
        {
            get { return item; }
        }

        public CollectionItemState State
        {
            get { return state; }
        }

        public object Sequence
        {
            get { return sequence; }
        }
    }
}
