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
        protected ValueBase(Guid parent)
            : this(parent, 0)
        {
        }

        protected ValueBase(Guid parent, uint sequence)
        {
            this.id = Guid.NewGuid();
            this.parent = parent;
            this.sequence = sequence;
            isNew = true;
        }

        protected ValueBase(Guid id, Guid parent)
            : this(id, parent, 0)
        {
        }

        protected ValueBase(Guid id, Guid parent, uint sequence)
        {
            this.id = id;
            this.parent = parent;
            this.sequence = sequence;
        }

        private readonly Guid id;
        private readonly Guid parent;
        private uint sequence;
        private bool isNew;
        private bool isMoved;
        private bool isRemoved;

        public Guid Id
        {
            get { return id; }
        }

        public Guid Parent
        {
            get { return parent; }
        }

        public uint Sequence
        {
            get { return sequence; }
        }

        public bool IsNew()
        {
            return isNew;
        }

        public bool IsMoved()
        {
            return isMoved;
        }

        public bool IsRemoved()
        {
            return isRemoved;
        }

        public virtual void Move(uint sequence)
        {
            if (this.sequence != sequence)
            {
                this.sequence = sequence;
                isMoved = true;
            }
        }

        public virtual void Remove()
        {
            isRemoved = true;
        }

        public virtual void Save()
        {
            isNew = false;
            isMoved = false;
            isRemoved = false;
        }
    }
}
