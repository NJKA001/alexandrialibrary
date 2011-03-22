using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface IStore
    {
        string Name { get; }
        void Execute(IBatch batch);
        void Execute(IEnumerable<IBatch> batches);
        ICollection<T> Execute<T>(IQuery<T> query);
        ICollection<T> Execute<T>(IEnumerable<IQuery<T>> queries);
    }
}
