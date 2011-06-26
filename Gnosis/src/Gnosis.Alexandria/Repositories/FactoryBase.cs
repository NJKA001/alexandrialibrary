using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class FactoryBase : IFactory
    {
        protected FactoryBase(IContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;

            MapValueConstructor<ITag>(() => new Tag());
        }

        private readonly IContext context;
        private readonly ILogger logger;
        private readonly IDictionary<Type, Func<IEntity>> entityConstructors = new Dictionary<Type, Func<IEntity>>();
        private readonly IDictionary<Type, Func<IChild>> childConstructors = new Dictionary<Type, Func<IChild>>();
        private readonly IDictionary<Type, Func<IValue>> valueConstructors = new Dictionary<Type, Func<IValue>>();

        protected IContext Context
        {
            get { return context; }
        }

        protected ILogger Logger
        {
            get { return logger; }
        }

        protected void MapEntityConstructor<T>(Func<IEntity> constructor)
            where T : IEntity
        {
            entityConstructors.Add(typeof(T), constructor);
        }

        protected void MapChildConstructor<T>(Func<IChild> constructor)
            where T : IChild
        {
            childConstructors.Add(typeof(T), constructor);
        }

        protected void MapValueConstructor<T>(Func<IValue> constructor)
            where T : IValue
        {
            valueConstructors.Add(typeof(T), constructor);
        }

        #region IFactory Members

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

        /*
        public void AddChildren(string childName, IEnumerable<IEntity> parents, IDictionary<Guid, IList<IChild>> children)
        {
            if (!addChildActions.ContainsKey(childName))
                throw new InvalidOperationException("No add action mapped for child name: " + childName);
            
            foreach (var parent in parents)
            {
                if (children.ContainsKey(parent.Id))
                {
                    foreach (var child in children[parent.Id].OrderBy(x => x.Sequence))
                        addChildActions[childName](parent, child);
                }
                //foreach (var child in children.Where(x => x.Parent == parent.Id).OrderBy(x => x.Sequence))
                //{
                //    addChildActions[childName](parent, child);
                //}
            }
        }

        public void AddValues(string valueName, IEnumerable<IEntity> parents, IDictionary<Guid, IList<IValue>> values)
        {
            if (!addValueActions.ContainsKey(valueName))
                throw new InvalidOperationException("No add action mapped for value name: " + valueName);

            foreach (var parent in parents)
            {
                if (values.ContainsKey(parent.Id))
                {
                    foreach (var value in values[parent.Id].OrderBy(x => x.Sequence))
                        addValueActions[valueName](parent, value);
                }
                //foreach (var value in values.Where(x => x.Parent == parent.Id).OrderBy(x => x.Sequence))
                //{
                //    addValueActions[valueName](parent, value);
                //}
            }
        }
        */

        #endregion
    }
}
