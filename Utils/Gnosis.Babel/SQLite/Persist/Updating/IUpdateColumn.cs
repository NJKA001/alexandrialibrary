using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public interface IUpdateColumn : IStatement, IUpdateColumnar, IFilterable
    {
    }

    public interface IUpdateColumn<T> : IStatement, IUpdateColumnar<T>, IFilterable<T>
        where T : IModel
    {
    }
}
