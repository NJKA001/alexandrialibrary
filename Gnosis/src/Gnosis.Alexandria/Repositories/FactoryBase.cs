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
        private readonly IDictionary<Type, Func<IDataRecord, IEntity>> createEntityFunctions = new Dictionary<Type, Func<IDataRecord, IEntity>>();
        private readonly IDictionary<Type, Func<IDataRecord, Guid, IChild>> createChildFunctions = new Dictionary<Type, Func<IDataRecord, Guid, IChild>>();
        private readonly IDictionary<Type, Func<IDataRecord, IValue>> createValueFunctions = new Dictionary<Type, Func<IDataRecord, IValue>>();
        private readonly IDictionary<Type, Dictionary<Type, Action<string, IEnumerable<IEntity>, IEnumerable<IChild>>>> addChildActions = new Dictionary<Type, Dictionary<Type, Action<string, IEnumerable<IEntity>, IEnumerable<IChild>>>>();
        private readonly IDictionary<Type, Dictionary<Type, Action<string, IEnumerable<IEntity>, IEnumerable<IValue>>>> addValueActions = new Dictionary<Type, Dictionary<Type, Action<string, IEnumerable<IEntity>, IEnumerable<IValue>>>>();

        protected IContext Context
        {
            get { return context; }
        }

        protected ILogger Logger
        {
            get { return logger; }
        }

        protected void MapCreateEntityFunction(Type type, Func<IDataRecord, IEntity> function)
        {
            createEntityFunctions.Add(type, function);
        }

        protected void MapCreateChildFunction(Type type, Func<IDataRecord, Guid, IChild> function)
        {
            createChildFunctions.Add(type, function);
        }

        protected void MapCreateValueFunction(Type type, Func<IDataRecord, IValue> function)
        {
            createValueFunctions.Add(type, function);
        }

        protected void MapAddChildAction(Type parentType, Type childType, Action<string, IEnumerable<IEntity>, IEnumerable<IChild>> action)
        {
            if (!addChildActions.ContainsKey(parentType))
                addChildActions.Add(parentType, new Dictionary<Type, Action<string, IEnumerable<IEntity>, IEnumerable<IChild>>>());

            addChildActions[parentType].Add(childType, action);
        }

        protected void MapAddValueAction(Type parentType, Type valueType, Action<string, IEnumerable<IEntity>, IEnumerable<IValue>> action)
        {
            if (!addValueActions.ContainsKey(parentType))
                addValueActions.Add(parentType, new Dictionary<Type, Action<string, IEnumerable<IEntity>, IEnumerable<IValue>>>());

            addValueActions[parentType].Add(valueType, action);
        }

        public IEntity CreateEntity(Type type)
        {
            if (createEntityFunctions.ContainsKey(type))
            {
                return createEntityFunctions[type](null);
            }

            return null;
        }

        public IEntity CreateEntity(Type type, IDataRecord record)
        {
            if (createEntityFunctions.ContainsKey(type))
            {
                return createEntityFunctions[type](record);
            }

            return null;
        }

        public IChild CreateChild(Type type, Guid parent)
        {
            if (createChildFunctions.ContainsKey(type))
            {
                return createChildFunctions[type](null, parent);
            }

            return null;
        }

        public IChild CreateChild(Type type, IDataRecord record)
        {
            if (createChildFunctions.ContainsKey(type))
            {
                return createChildFunctions[type](record, Guid.Empty);
            }

            return null;
        }

        public IValue CreateValue(Type type, IDataRecord record)
        {
            if (createValueFunctions.ContainsKey(type))
            {
                return createValueFunctions[type](record);
            }

            return null;
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
            if (addChildActions.ContainsKey(parentType))
            {
                var map = addChildActions[parentType];
                if (map.ContainsKey(childType))
                {
                    map[childType](childName, parents, children);
                }
            }
        }

        public void AddValues(Type parentType, Type valueType, string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> values)
        {
            if (addValueActions.ContainsKey(parentType))
            {
                var map = addValueActions[parentType];
                if (map.ContainsKey(valueType))
                {
                    map[valueType](valueName, parents, values);
                }
            }
        }
    }
}
