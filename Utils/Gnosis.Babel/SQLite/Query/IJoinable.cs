namespace Gnosis.Babel.SQLite.Query
{
    public interface IJoinable
    {
        IJoin InnerJoin(string table, string alias);
        IJoin LeftOuterJoin(string table, string alias);
    }
}
