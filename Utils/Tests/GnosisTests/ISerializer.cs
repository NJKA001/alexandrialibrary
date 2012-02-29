using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests
{
    public interface ISerializer<T>
        where T : IEntity
    {
        T Deserialize(string[] data);
        string Serialize(T entity);
        string SerializeUpdate(T entity, string field, object value);

        void ApplyUpdate(T entity, string field, string value);
    }
}
