using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class FactoryBase : IFactory
    {
        protected FactoryBase(IContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        private readonly IContext context;
        private readonly ILogger logger;
        private readonly IDictionary<Type, Func<IEntity>> entityConstructors = new Dictionary<Type, Func<IEntity>>();
        private readonly IDictionary<Type, Func<IChild>> childConstructors = new Dictionary<Type, Func<IChild>>();
        private readonly IDictionary<Type, Func<IValue>> valueConstructors = new Dictionary<Type, Func<IValue>>();
        private readonly IDictionary<string, Action<IEntity, IChild>> addChildActions = new Dictionary<string, Action<IEntity, IChild>>();
        private readonly IDictionary<string, Action<IEntity, IValue>> addValueActions = new Dictionary<string, Action<IEntity, IValue>>();

        protected IContext Context
        {
            get { return context; }
        }

        protected ILogger Logger
        {
            get { return logger; }
        }

        protected void MapEntityConstructor(Type type, Func<IEntity> constructor)
        {
            entityConstructors.Add(type, constructor);
        }

        protected void MapChildConstructor(Type type, Func<IChild> constructor)
        {
            childConstructors.Add(type, constructor);
        }

        protected void MapValueConstructor(Type type, Func<IValue> constructor)
        {
            valueConstructors.Add(type, constructor);
        }

        protected void MapAddChildAction(string childName, Action<IEntity, IChild> action)
        {
            addChildActions.Add(childName, action);
        }

        protected void MapAddValueAction(string valueName, Action<IEntity, IValue> action)
        {
            addValueActions.Add(valueName, action);
        }

        public IEntity CreateEntity(Type type)
        {
            if (!entityConstructors.ContainsKey(type))
                throw new InvalidOperationException("No constructor mapped for entity type: " + type.Name);

            var entity = entityConstructors[type]();
            entity.Initialize(new EntityInitialState(Context, Logger));
            return entity;
        }

        public IEntity CreateEntity(Type type, IDataRecord record)
        {
            if (!entityConstructors.ContainsKey(type))
                throw new InvalidOperationException("No constructor mapped for entity type: " + type.Name);

            var entity = entityConstructors[type]();
            entity.Initialize(new EntityInitialState(Context, Logger, record));
            return entity;
        }

        public IChild CreateChild(Type type, Guid parent)
        {
            if (!childConstructors.ContainsKey(type))
                throw new InvalidOperationException("No constructor mapped for child type: " + type.Name);

            var child = childConstructors[type]();
            child.Initialize(new EntityInitialState(Context, Logger, parent));
            return child;
        }

        public IChild CreateChild(Type type, IDataRecord record)
        {
            if (!childConstructors.ContainsKey(type))
                throw new InvalidOperationException("No constructor mapped for child type: " + type.Name);

            var child = childConstructors[type]();
            child.Initialize(new EntityInitialState(Context, Logger, record));
            return child;
        }

        public IValue CreateValue(Type type, IDataRecord record)
        {
            if (!valueConstructors.ContainsKey(type))
                throw new InvalidOperationException("No constructor mapped for value type: " + type.Name);

            var value = valueConstructors[type]();
            value.Initialize(new ValueInitialState(record));
            return value;
        }

        public T CreateEntity<T>()
            where T : IEntity
        {
            return (T)CreateEntity(typeof(T));
        }

        public T CreateEntity<T>(IDataRecord record)
            where T : IEntity
        {
            return (T)CreateEntity(typeof(T), record);
        }

        public T CreateChild<T>(Guid parent)
            where T : IChild
        {
            return (T)CreateChild(typeof(T), parent);
        }

        public T CreateChild<T>(IDataRecord record) where T : IChild
        {
            return (T)CreateChild(typeof(T), record);
        }

        public T CreateValue<T>(IDataRecord record) where T : IValue
        {
            return (T)CreateValue(typeof(T), record);
        }

        public void AddChildren(Type parentType, Type childType, string childName, IEnumerable<IEntity> parents, IEnumerable<IChild> children)
        {
            if (!addChildActions.ContainsKey(childName))
                throw new InvalidOperationException("No add action mapped for child name: " + childName);
            
            foreach (var parent in parents)
            {
                foreach (var child in children.Where(x => x.Parent == parent.Id).OrderBy(x => x.Sequence))
                {
                    addChildActions[childName](parent, child);
                }
            }
        }

        public void AddValues(Type parentType, Type valueType, string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> values)
        {
            if (!addValueActions.ContainsKey(valueName))
                throw new InvalidOperationException("No add action mapped for value name: " + valueName);

            foreach (var parent in parents)
            {
                foreach (var value in values.Where(x => x.Parent == parent.Id).OrderBy(x => x.Sequence))
                {
                    addValueActions[valueName](parent, value);
                }
            }
        }
    }
}
