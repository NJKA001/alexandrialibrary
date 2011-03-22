using System.Data;

namespace Gnosis.Babel
{
    public interface IModelMapper<T>
    {
        object GetId(IDataRecord record);
        T GetModel(IDataRecord record);
    }
}
