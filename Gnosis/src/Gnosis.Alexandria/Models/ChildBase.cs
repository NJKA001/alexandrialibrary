using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public class ChildBase
        : EntityBase, IChild
    {
        protected ChildBase(IContext context, Guid parent)
            : base(context)
        {
            this.parent = parent;
        }

        protected ChildBase(IContext context, Guid id, DateTime timeStamp, Guid parent)
            : base(context, id, timeStamp)
        {
            this.parent = parent;
        }

        protected ChildBase(IContext context, Guid id, DateTime timeStamp, Guid parent, uint sequence)
            : base(context, id, timeStamp)
        {
            this.parent = parent;
            this.sequence = sequence;
        }

        private readonly Guid parent;
        private uint sequence;
        private bool isMoved;
        private bool isRemoved;

        public Guid Parent
        {
            get { return parent; }
        }

        public uint Sequence
        {
            get { return sequence; }
        }

        public bool IsMoved()
        {
            return isMoved;
        }

        public bool IsRemoved()
        {
            return isRemoved;
        }

        public void Move(uint sequence)
        {
            if (this.sequence != sequence)
            {
                this.sequence = sequence;
                isMoved = true;
            }
        }

        public void Remove()
        {
            isRemoved = true;
        }

        public override void Save(DateTime timeStamp)
        {
            base.Save(timeStamp);
            isMoved = false;
        }
    }
}
