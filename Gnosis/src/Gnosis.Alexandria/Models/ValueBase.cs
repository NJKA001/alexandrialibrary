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
        private Guid id;
        private Guid parent;
        private uint sequence;
        private bool isInitialized;
        private bool isNew;
        private bool isMoved;
        private bool isRemoved;
        private readonly IDictionary<string, Action<object>> initializers = new Dictionary<string, Action<object>>();

        protected void AddInitializer(string name, Action<object> action)
        {
            initializers[name] = action;
        }

        protected void Initialize(Guid parent)
        {
            Initialize(parent, 0);
        }

        protected void Initialize(Guid parent, uint sequence)
        {
            this.id = Guid.NewGuid();
            this.parent = parent;
            this.sequence = sequence;
            this.isNew = true;
            this.isInitialized = true;

            foreach (var initializer in initializers)
                initializer.Value(null);
        }

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

        public bool IsInitialized()
        {
            return isInitialized;
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
            if (!isInitialized)
                throw new InvalidOperationException("Value must be initialized before it can be moved");

            if (this.sequence != sequence)
            {
                this.sequence = sequence;
                isMoved = true;
            }
        }

        public virtual void Remove()
        {
            if (!isInitialized)
                throw new InvalidOperationException("Value must be initialized before it can be removed");

            isRemoved = true;
        }

        public virtual void Initialize(IValueInitialState state)
        {
            this.id = state.Id;
            this.parent = state.Parent;
            this.sequence = state.Sequence;
            this.isNew = state.IsNew;
            this.isInitialized = true;

            if (!isNew)
            {
                foreach (var initializer in initializers)
                    state.Initialize(initializer.Key, initializer.Value);
            }
        }

        public virtual void Save()
        {
            isNew = false;
            isMoved = false;
            isRemoved = false;
        }
    }
}
