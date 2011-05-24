using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public struct CollectionItemInfo
    {
        public CollectionItemInfo(object item, CollectionItemState state, object id)
        {
            this.item = item;
            this.state = state;
            this.id = id;
            this.sequence = null;
        }

        public CollectionItemInfo(object item, CollectionItemState state, object id, object sequence)
        {
            this.item = item;
            this.state = state;
            this.id = id;
            this.sequence = sequence;
        }

        private readonly object item;
        private readonly CollectionItemState state;
        private readonly object id;
        private readonly object sequence;

        public object Item
        {
            get { return item; }
        }

        public CollectionItemState State
        {
            get { return state; }
        }

        public object Id
        {
            get { return id; }
        }

        public object Sequence
        {
            get { return sequence; }
        }
    }
}
