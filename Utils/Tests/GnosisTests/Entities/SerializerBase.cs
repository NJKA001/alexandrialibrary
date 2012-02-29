using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests.Entities
{
    public abstract class SerializerBase<T>
        : ISerializer<T>
        where T : IEntity
    {
        public abstract string Serialize(T entity);
        public abstract string SerializeUpdate(T entity, string field, object value);
        public abstract T Deserialize(string[] data);

        public abstract void ApplyUpdate(T entity, string field, string value);
    }
}
