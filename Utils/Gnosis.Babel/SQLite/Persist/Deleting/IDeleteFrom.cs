namespace Gnosis.Babel.SQLite.Persist.Deleting
{
    public interface IDeleteFrom : IStatement, IFilterable
    {
    }

    public interface IDeleteFrom<T> : IStatement, IFilterable<T>
        where T : IModel
    {
    }
}
