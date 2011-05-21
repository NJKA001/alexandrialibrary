using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public struct CollectionItemInfo
    {
        public CollectionItemInfo(object sequence, object item, CollectionItemState state)
        {
            this.sequence = sequence;
            this.item = item;
            this.state = state;
        }

        private readonly object sequence;
        private readonly object item;
        private readonly CollectionItemState state;

        public object Sequence
        {
            get { return sequence; }
        }

        public object Item
        {
            get { return item; }
        }

        public CollectionItemState State
        {
            get { return state; }
        }
    }
}
