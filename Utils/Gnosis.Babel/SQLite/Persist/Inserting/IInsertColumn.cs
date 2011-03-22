using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertColumn : IStatement, IInsertColumnar, IInsertValued
    {
        ISelect SelectAll { get; }
        ISelect SelectDistinct { get; }
    }

    public interface IInsertColumn<T> : IStatement, IInsertColumnar<T>, IInsertValued<T>
        where T : IModel
    {
        ISelect SelectAll { get; }
        ISelect SelectDistinct { get; }
    }
}
