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
    public abstract class EntityBase<T>
        : IEntity
        where T : IEntity
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
        private readonly IDictionary<Uri, Func<string, IHashCode>> hashFunctions = new Dictionary<Uri, Func<string, IHashCode>>();
        private readonly IDictionary<string, Tuple<Action<IHashCode>, Action<IHashCode>>> hashInitializers = new Dictionary<string, Tuple<Action<IHashCode>, Action<IHashCode>>>();

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

        protected void Change(Action action, Expression<Func<T, object>> property)
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before it can be changed");

            context.Invoke(action);

            isChanged = true;

            var propertyInfo = property.AsProperty();
            OnPropertyChanged(propertyInfo.Name);
        }

        protected void AddInitializer(Action<object> action, Expression<Func<T, object>> property)
        {
            var propertyInfo = property.AsProperty();
            initializers[propertyInfo.Name] = action;
        }

        protected void AddInitializer(Action<string, IDataRecord> action, Expression<Func<T, object>> property)
        {
            var propertyInfo = property.AsProperty();
            customInitializers[propertyInfo.Name] = action;
        }

        protected void AddChildInitializer<TChild>(Action<IChild> action)
            where TChild : IChild
        {
            var childName = typeof(TChild).GetNormalizedName();
            childInitializers[childName] = action;
        }

        protected void AddValueInitializer(Action<IValue> action, Expression<Func<T, object>> property)
        {
            var propertyInfo = property.AsProperty();
            var entityName = typeof(T).GetNormalizedName();
            var name = string.Format("{0}_{1}", entityName, propertyInfo.Name);
            valueInitializers[name] = action;
        }

        protected void AddChild<TChild>(Action action, TChild child, Expression<Func<T, object>> property)
            where TChild : IChild
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before children can be added");

            var propertyInfo = property.AsProperty();
            var parentInfo = new EntityInfo(typeof(T));
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

        protected void RemoveChild<TChild>(Action action, TChild child, Expression<Func<T, object>> property)
            where TChild : IChild
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before children can be removed");

            var propertyInfo = property.AsProperty();
            var parentInfo = new EntityInfo(typeof(T));
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

        protected void AddValue<TValue>(Action action, TValue value, Expression<Func<T, object>> property)
            where TValue : IValue
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before values can be added");

            var entity = new EntityInfo(typeof(T));
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

        protected void RemoveValue<TValue>(Action action, TValue value, Expression<Func<T, object>> property)
            where TValue : IValue
        {
            if (!isInitialized)
                throw new InvalidOperationException("Entity must be initialized before values can be removed");

            var entity = new EntityInfo(typeof(T));
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

        protected void AddHashFunction(Uri scheme, Func<string, IHashCode> function)
        {
            hashFunctions[scheme] = function;
        }

        protected void AddHashInitializer(Action<IHashCode> addFunction, Action<IHashCode> removeFunction, Expression<Func<T, IEnumerable<IHashCode>>> property)
        {
            var propertyInfo = property.AsProperty();

            hashInitializers[propertyInfo.Name] = new Tuple<Action<IHashCode>,Action<IHashCode>>(addFunction, removeFunction);
        }

        protected void RefreshHashCodes(string value, Expression<Func<T, IEnumerable<IHashCode>>> property)
        {
            RefreshHashCodes(value, property, null);
        }

        protected void RefreshHashCodes(string value, Expression<Func<T, IEnumerable<IHashCode>>> property, params Uri[] schemesToUse)
        {
            if (hashFunctions.Count == 0)
                throw new InvalidOperationException("No hash functions defined for this entity");

            var propertyInfo = property.AsProperty();
            if (!hashInitializers.ContainsKey(propertyInfo.Name))
                throw new InvalidOperationException("No hash initializer defined for property: " + propertyInfo.Name);

            var source = propertyInfo.GetValue(this, null) as IEnumerable<IHashCode>;
            if (source == null)
                throw new InvalidOperationException("Property is not a valid hash source: " + propertyInfo.Name);

            var addAction = hashInitializers[propertyInfo.Name].Item1;
            var removeAction = hashInitializers[propertyInfo.Name].Item2;

            foreach (var scheme in hashFunctions.Keys)
            {
                if (schemesToUse == null || schemesToUse.Contains(scheme))
                {
                    var oldCodes = new List<IHashCode>();
                    oldCodes.AddRange(source.Where(hashCode => hashCode.Scheme == scheme));

                    foreach (var hashCode in oldCodes)
                        removeAction(hashCode);

                    var rootCode = hashFunctions[scheme](value);
                    if (rootCode != null)
                    {
                        addAction(rootCode);

                        var tokens = value.ToTokens();
                        if (tokens.Count() > 1)
                        {
                            foreach (var token in tokens)
                            {
                                var hashCode = hashFunctions[scheme](token);
                                if (hashCode != null && hashCode.Value != value)
                                    addAction(hashCode);
                            }
                        }
                    }
                }
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
