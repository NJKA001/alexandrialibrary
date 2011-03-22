namespace Gnosis.Babel.SQLite.Query
{
    public interface ICompound : IStatement
    {
        ISelect Select { get; }
    }
}
