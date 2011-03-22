using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITriggerType : IStatement
    {
        ITriggerBody Begin { get; }
        IPredicate<ITriggerWhen> When(string expression);
    }

    public interface ITriggerType<T> : IStatement
    {
        ITriggerBody<T> Begin { get; }
        IPredicate<ITriggerWhen<T>> When(Expression<Func<T, object>> expression);
    }
}
