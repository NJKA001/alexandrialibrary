namespace Gnosis.Babel
{
    public interface IQueryMapper<T>
    {
        IQuery<T> GetSelectOneQuery(object id);
        IQuery<T> GetSelectAllQuery();
    }
}
