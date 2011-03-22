namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public interface IUpdateSet : IStatement
    {
        IUpdateColumn Set { get; }
    }

    public interface IUpdateSet<T> : IStatement
        where T : IModel
    {
        IUpdateColumn<T> Set { get; }
    }
}
