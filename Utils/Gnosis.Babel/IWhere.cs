using System;
using System.Linq.Expressions;

namespace Gnosis.Babel
{
    public interface IWhere : IStatement
    {
        IPredicate<IWhere> And<T>(Expression<Func<T, object>> property);
        IPredicate<IWhere> Or<T>(Expression<Func<T, object>> property);
    }
}
