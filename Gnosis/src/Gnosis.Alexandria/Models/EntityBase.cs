using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly IDictionary<string, IDictionary<Guid, IChild>> children = new Dictionary<string, IDictionary<Guid, IChild>>();
        private readonly IDictionary<string, IDictionary<Guid, IChild>> removedChildren = new Dictionary<string, IDictionary<Guid, IChild>>();
        private readonly IDictionary<string, IDictionary<Guid, IValue>> values = new Dictionary<string, IDictionary<Guid, IValue>>();
        private readonly IDictionary<string, IDictionary<Guid, IValue>> removedValues = new Dictionary<string, IDictionary<Guid, IValue>>();
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

        protected void AddChild<TParent, TChild>(Action action, TChild child, Expression<Func<TParent, object>> property)
            where TParent : IEntity
            where TChild : IChild
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before children can be added");

            var propertyInfo = property.AsProperty();
            var parentInfo = new EntityInfo(typeof(TParent));
            var childInfo = new EntityInfo(typeof(TChild), parentInfo);
            var key = childInfo.Name;

            if (!removedChildren.ContainsKey(key))
                removedChildren[key] = new Dictionary<Guid, IChild>();

            if (!children.ContainsKey(key))
                children[key] = new Dictionary<Guid, IChild>();

            if (removedChildren[key].ContainsKey(child.Id))
                removedChildren[key].Remove(child.Id);

            if (!children[key].ContainsKey(child.Id))
            {
                context.Invoke(action);
                children[key].Add(child.Id, child);
                OnPropertyChanged(propertyInfo.Name);
            }
        }

        protected void RemoveChild<TParent, TChild>(Action action, TChild child, Expression<Func<TParent, object>> property)
            where TParent : IEntity
            where TChild : IChild
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before children can be removed");

            var propertyInfo = property.AsProperty();
            var parentInfo = new EntityInfo(typeof(TParent));
            var childInfo = new EntityInfo(typeof(TChild), parentInfo);
            var key = childInfo.Name;

            if (!removedChildren.ContainsKey(key))
                removedChildren[key] = new Dictionary<Guid, IChild>();

            if (!children.ContainsKey(key))
                children[key] = new Dictionary<Guid, IChild>();

            if (children[key].ContainsKey(child.Id))
            {
                var childToRemove = children[key][child.Id];
                context.Invoke(action);
                childToRemove.Remove();
                children[key].Remove(child.Id);
                removedChildren[key].Add(childToRemove.Id, childToRemove);
                OnPropertyChanged(propertyInfo.Name);
            }
        }

        protected void AddValue<TParent, TValue>(Action action, TValue value, Expression<Func<TParent, object>> property)
            where TParent : IEntity
            where TValue : IValue
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before values can be added");

            var entity = new EntityInfo(typeof(TParent));
            var propertyInfo = property.AsProperty();
            var key = string.Format("{0}_{1}", entity.Name, propertyInfo.Name);

            if (!removedValues.ContainsKey(key))
                removedValues[key] = new Dictionary<Guid, IValue>();

            if (!values.ContainsKey(key))
                values[key] = new Dictionary<Guid, IValue>();

            if (removedValues[key].ContainsKey(value.Id))
                removedValues[key].Remove(value.Id);

            if (!values[key].ContainsKey(value.Id))
            {
                context.Invoke(action);
                values[key].Add(value.Id, value);
                OnPropertyChanged(propertyInfo.Name);
            }
        }

        protected void RemoveValue<TParent, TValue>(Action action, TValue value, Expression<Func<TParent, object>> property)
            where TParent : IEntity
            where TValue : IValue
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before values can be removed");

            var entity = new EntityInfo(typeof(TParent));
            var propertyInfo = property.AsProperty();

            var key = string.Format("{0}_{1}", entity.Name, propertyInfo.Name);

            if (!removedValues.ContainsKey(key))
                removedValues[key] = new Dictionary<Guid, IValue>();

            if (!values.ContainsKey(key))
                values[key] = new Dictionary<Guid, IValue>();

            if (values[key].ContainsKey(value.Id))
            {
                var valueToRemove = values[key][value.Id];
                context.Invoke(action);
                valueToRemove.Remove();
                values[key].Remove(value.Id);
                removedValues[key].Add(value.Id, valueToRemove);
                OnPropertyChanged(propertyInfo.Name);
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
            var allChildren = new List<IChild>();
            if (removedChildren.ContainsKey(childInfo.Name))
                allChildren.AddRange(removedChildren[childInfo.Name].Values);
            if (children.ContainsKey(childInfo.Name))
                allChildren.AddRange(children[childInfo.Name].Values);

            return allChildren;
        }

        public virtual IEnumerable<IValue> GetValues(ValueInfo valueInfo)
        {
            var allValues = new List<IValue>();
            if (removedValues.ContainsKey(valueInfo.Name))
                allValues.AddRange(removedValues[valueInfo.Name].Values);
            if (values.ContainsKey(valueInfo.Name))
                allValues.AddRange(values[valueInfo.Name].Values);

            return allValues;
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
