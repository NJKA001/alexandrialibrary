using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IFactory
    {
        IEntity CreateEntity(Type type);
        IEntity CreateEntity(Type type, IDataRecord record);
        IChild CreateChild(Type type, Guid parent);
        IChild CreateChild(Type type, IDataRecord record);
        IValue CreateValue(Type type, IDataRecord record);

        T CreateEntity<T>() where T : IEntity;
        T CreateEntity<T>(IDataRecord record) where T : IEntity;
        T CreateChild<T>(Guid parent) where T : IChild;
        T CreateChild<T>(IDataRecord record) where T : IChild;
        T CreateValue<T>(IDataRecord record) where T : IValue;
    }
}
