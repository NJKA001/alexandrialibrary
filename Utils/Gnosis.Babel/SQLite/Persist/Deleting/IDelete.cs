namespace Gnosis.Babel.SQLite.Persist.Deleting
{
    public interface IDelete : IStatement
    {
        IDeleteFrom From(string table);
    }

    public interface IDelete<T> : IStatement
        where T : IModel
    {
        IDeleteFrom<T> From(string table);
    }
}
