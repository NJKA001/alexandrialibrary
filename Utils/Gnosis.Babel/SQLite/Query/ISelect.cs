namespace Gnosis.Babel.SQLite.Query
{
    public interface ISelect : IStatement
    {
        IResult All { get; }
        IResult Distinct { get; }
        IResult LastInsertRowId { get; }
    }
}
