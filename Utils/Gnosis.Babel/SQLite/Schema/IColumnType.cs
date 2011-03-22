namespace Gnosis.Babel.SQLite.Schema
{
    public interface IColumnType : IStatement, IColumnConstrained
    {
    }

    public interface IColumnType<T> : IStatement, IColumnConstrained<T>
    {
    }
}
