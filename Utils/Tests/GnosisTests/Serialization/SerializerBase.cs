using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests.Serialization
{
    public abstract class SerializerBase<T>
    {
        public abstract string Serialize(T item);
        public abstract T Deserialize(string[] data);
    }
}
