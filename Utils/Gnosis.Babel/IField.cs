using System;
using System.Linq.Expressions;

namespace Gnosis.Babel
{
    public interface IField<T>
    {
        string Name { get; }
        Expression<Func<T, object>> Getter { get; }
        Action<T, object> Setter { get; }
    }
}
