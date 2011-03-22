using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITable : IStatement, IColumnar
    {
        ISelect AsSelect { get; }
    }

    public interface ITable<T> : IStatement, IColumnar<T>
    {
        ISelect AsSelect { get; }
    }
}
