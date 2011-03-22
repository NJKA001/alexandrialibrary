namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsert : IStatement, IInsertable, IConflicting<IInsertConflictClause>
    {
    }

    public interface IInsert<T> : IStatement, IInsertable<T>, IConflicting<IInsertConflictClause<T>>
        where T : IModel
    {
    }
}
