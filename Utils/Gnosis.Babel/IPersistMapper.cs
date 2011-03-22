using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface IPersistMapper<T>
    {
        IBatch GetPersistBatch(T model);
    }
}
