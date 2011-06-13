using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public abstract class EntityBase
        : IEntity
    {
        private IContext context;
        private ILogger logger;
        private Guid id;
        private DateTime timeStamp;
        private bool isNew;
        private bool isChanged;
        private bool isInitialized;
        private readonly IDictionary<Guid, IChild> children = new Dictionary<Guid, IChild>();
        private readonly IDictionary<Guid, IChild> removedChildren = new Dictionary<Guid, IChild>();
        private readonly IDictionary<Guid, IValue> values = new Dictionary<Guid, IValue>();
        private readonly IDictionary<Guid, IValue> removedValues = new Dictionary<Guid, IValue>();
        private readonly IDictionary<string, Action<object>> initializers = new Dictionary<string, Action<object>>();
        private readonly IDictionary<string, Action<string, IDataRecord>> customInitializers = new Dictionary<string, Action<string, IDataRecord>>();
        private readonly IDictionary<string, Action<IChild>> childInitializers = new Dictionary<string, Action<IChild>>();
        private readonly IDictionary<string, Action<IValue>> valueInitializers = new Dictionary<string, Action<IValue>>();

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                catch (Exception ex)
                {
                    logger.Error("EntityBase.OnPropertyChanged", ex);
                }
            }
        }

        protected IContext Context
        {
            get { return context; }
        }

        protected ILogger Logger
        {
            get { return logger; }
        }

        protected void Change(Action action, string propertyName)
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before it can be changed");

            context.Invoke(action);

            isChanged = true;

            OnPropertyChanged(propertyName);
        }

        protected void AddInitializer(string name, Action<object> action)
        {
            initializers[name] = action;
        }

        protected void AddInitializer(string name, Action<string, IDataRecord> action)
        {
            customInitializers[name] = action;
        }

        protected void AddChildInitializer(string name, Action<IChild> action)
        {
            childInitializers[name] = action;
        }

        protected void AddValueInitializer(string name, Action<IValue> action)
        {
            valueInitializers[name] = action;
        }

        protected void AddChild(Action action, IChild child, string propertyName)
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before children can be added");

            if (removedChildren.ContainsKey(child.Id))
                removedChildren.Remove(child.Id);

            if (!children.ContainsKey(child.Id))
            {
                context.Invoke(action);
                children.Add(child.Id, child);
                OnPropertyChanged(propertyName);
            }
        }

        protected void RemoveChild(Action action, Guid id, string propertyName)
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before children can be removed");

            if (children.ContainsKey(id))
            {
                var child = children[id];
                context.Invoke(action);
                child.Remove();
                children.Remove(id);
                removedChildren.Add(id, child);
                OnPropertyChanged(propertyName);
            }
        }

        protected void AddValue(Action action, IValue value, string propertyName)
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before values can be added");

            if (removedValues.ContainsKey(value.Id))
                removedValues.Remove(value.Id);

            if (!values.ContainsKey(value.Id))
            {
                context.Invoke(action);
                values.Add(value.Id, value);
                OnPropertyChanged(propertyName);
            }
        }

        protected void RemoveValue(Action action, Guid id, string propertyName)
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before values can be removed");

            if (values.ContainsKey(id))
            {
                var value = values[id];
                context.Invoke(action);
                value.Remove();
                values.Remove(id);
                removedValues.Add(id, value);
                OnPropertyChanged(propertyName);
            }
        }

        public Guid Id
        {
            get { return id; }
        }

        public DateTime TimeStamp
        {
            get { return timeStamp; }
        }

        public bool IsNew()
        {
            return isNew;
        }

        public bool IsChanged()
        {
            return isChanged;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }

        public virtual IEnumerable<IChild> GetChildren(EntityInfo childInfo)
        {
            return (removedChildren.Values.Concat(children.Values)).Where(x => childInfo.Type.IsAssignableFrom(x.GetType()));
        }

        public virtual IEnumerable<IValue> GetValues(ValueInfo valueInfo)
        {
            return (removedValues.Values.Concat(values.Values)).Where(x => valueInfo.Type.IsAssignableFrom(x.GetType()));
        }

        public virtual void Initialize(IEntityInitialState state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            this.context = state.Context;
            this.logger = state.Logger;
            this.id = state.Id;
            this.timeStamp = state.TimeStamp;
            this.isNew = state.IsNew;
            this.isInitialized = true;

            if (!isNew)
            {
                foreach (var initializer in initializers)
                    state.Initialize(initializer.Key, initializer.Value);

                foreach (var customInitializer in customInitializers)
                    state.Initialize(customInitializer.Key, customInitializer.Value);
            }
        }

        public virtual void InitializeChildren(string name, IEnumerable<IChild> children)
        {
            if (!childInitializers.ContainsKey(name))
                throw new InvalidOperationException("No child initializer with name: " + name);

            foreach (var child in children)
                childInitializers[name](child);
        }

        public virtual void InitializeValues(string name, IEnumerable<IValue> values)
        {
            if (!valueInitializers.ContainsKey(name))
                throw new InvalidOperationException("No value initializer with name: " + name);


            foreach (var value in values)
                valueInitializers[name](value);
        }

        public virtual void Save(DateTime timeStamp)
        {
            this.timeStamp = timeStamp;
            isNew = false;
            isChanged = false;
            removedChildren.Clear();
            removedValues.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
