using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public class ChildBase<TParent, TChild>
        : EntityBase<TChild>, IChild
        where TParent :IEntity
        where TChild : IChild
    {
        private Guid parent;
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

        public override void Initialize(IEntityInitialState state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            this.parent = state.Parent;
            this.sequence = state.Sequence;

            base.Initialize(state);
        }

        public override void Save(DateTime timeStamp)
        {
            base.Save(timeStamp);
            isMoved = false;
        }
    }
}
