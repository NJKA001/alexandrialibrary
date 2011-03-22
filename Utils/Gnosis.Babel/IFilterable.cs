using System;
using System.Linq.Expressions;

namespace Gnosis.Babel
{
    public interface IFilterable
    {
        IPredicate<IWhere> Where<T>(Expression<Func<T, object>> property);
    }

    public interface IFilterable<T>
        where T : IModel
    {
        IPredicate<IWhere> Where<TOther>(Expression<Func<TOther, object>> property) where TOther : IModel;
        IPredicate<IWhere> Where(Expression<Func<T, object>> property);
    }
}
