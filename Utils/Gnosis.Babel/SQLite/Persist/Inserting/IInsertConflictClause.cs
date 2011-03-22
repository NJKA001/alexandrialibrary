namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertConflictClause : IStatement, IInsertable
    {
    }

    public interface IInsertConflictClause<T> : IStatement, IInsertable<T>
        where T : IModel
    {
    }
}
