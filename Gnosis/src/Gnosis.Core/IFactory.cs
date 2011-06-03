using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IFactory
    {
        T Create<T>();
        T Create<T>(IDataRecord record);
        object Create(Type type);
        object Create(Type type, IDataRecord record);
    }
}
