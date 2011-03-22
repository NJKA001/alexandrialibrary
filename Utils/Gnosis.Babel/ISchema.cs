using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface ISchema<T>
    {
        string Name { get; }
        IEnumerable<IField<T>> Fields { get; }
        IEnumerable<IField<T>> NonPrimaryFields { get; }
        IField<T> PrimaryField { get; }
        IEnumerable<IKey<T>> Keys { get; }
    }
}
