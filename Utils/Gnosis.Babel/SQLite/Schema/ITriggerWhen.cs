using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITriggerWhen : IStatement
    {
        ITriggerBody Begin { get; }
        IPredicate<ITriggerWhen> And(string expression);
        IPredicate<ITriggerWhen> Or(string expression);
    }

    public interface ITriggerWhen<T> : IStatement
    {
        ITriggerBody<T> Begin { get; }
        IPredicate<ITriggerWhen<T>> And(Expression<Func<T, object>> expression);
        IPredicate<ITriggerWhen<T>> Or(Expression<Func<T, object>> expression);
    }
}
